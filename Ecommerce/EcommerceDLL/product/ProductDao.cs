using Helpers.connection;
using Helpers.converter;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Text;

namespace OpenEcommerceDLL.product
{
    public class ProductDao
    {
        Connection con = new Connection();
        public Product Save(Product produto)
        {
            produto = SaveProduct(produto);
            
            return produto;
        }

        private Product SaveImage(Product product)
        {
            foreach (ProductImage i in product.Images)
            {
                using (DBSave save = new DBSave(con, ProductImage.table))
                {
                    i.ImageType = Converter.getImageTypeFromHtmlBase64(i.Image);
                    save.addKey("codProduto", product.ProductId);
                    save.addPar("imagem", Convert.FromBase64String(i.Image.Replace($"data:image/{i.ImageType};base64,", "")));
                    save.addPar("imageType", i.ImageType);
                    save.deleteInsert();
                }
            }
            return product;
        }

        private Product SaveProduct(Product product)
        {
            bool exists = false;
            using (DBSave save = new DBSave(con, Product.table))
            {
                if (product.ProductId > 0)
                {
                    save.addKey("codProduto", product.ProductId);
                    exists = true;
                }
                save.addPar("codVendedor", product.SalesPerson.UserId);
                save.addPar("codCategoria", product.Category.CategoryId);
                save.addPar("codTipo", product.Type.TypeId);
                save.addPar("codMarca", product.Brand.BrandId);
                save.addPar("nome", product.Name);
                save.addPar("descricao", product.Description);
                save.addPar("codEstadoUso", product.State.ProductUseStateId);
                save.addPar("preco", product.Price);
                save.addPar("quantidade", product.Amount);
                save.save(exists);
            }
            if (!exists)
            {
                product.ProductId = DbHelper.GetLastId(Product.table, "codProduto", con);
            }
            product = SaveImage(product);
            return product;
        }

        private Product getimagesProduct(Product product)
        {
            using (IDataReader dr = DbHelper.ReturnDataReader($"SELECT imagem, imageType FROM {ProductImage.table} WHERE codProduto = {product.ProductId}", con))
            {
                while (dr.Read())
                {
                    string base64Encoded = Converter.tryToBase64String((byte[])dr["imagem"]);
                    ProductImage i = new ProductImage();
                    i.ImageType = Converter.tryToString(dr["imageType"]);
                    i.Image = Converter.tryToString($"data:image/{i.ImageType};base64," + base64Encoded);
                    product.Images.Add(i);
                }
            }
            return product;
        }

        private Product drToProd(IDataReader dr)
        {
            Product p = new Product();
            p.ProductId = Converter.tryToInt(dr["codProduto"]);
            p.Name = Converter.tryToString(dr["nome"]);
            p.Description = Converter.tryToString(dr["descricao"]);
            p.Category.CategoryId = Converter.tryToInt(dr["codCategoria"]);
            p.Category.Name = Converter.tryToString(dr["nomeCategoria"]);
            p.State.ProductUseStateId = Converter.tryToInt(dr["codEstadoUso"]);
            p.State.Description = Converter.tryToString(dr["nomeEstadoUso"]);
            p.Brand.BrandId = Converter.tryToInt(dr["codMarca"]);
            p.Brand.Description = Converter.tryToString(dr["nomeMarca"]);
            p.Type.TypeId = Converter.tryToInt(dr["codTipo"]);
            p.Type.Description = Converter.tryToString(dr["nomeTipo"]);
            p.SalesPerson.UserId = Converter.tryToString(dr["codVendedor"]);
            p.SalesPerson.UserId = Converter.tryToString(dr["nomeVendedor"]);
            p.Amount = Converter.tryToDouble(dr["quantidade"]);
            p.Price = Converter.tryToDouble(dr["preco"]);
            return p;
        }

        private string GetSqlProducts(ProductFilter pf)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT PROD.codProduto");
            sql.AppendLine(", PROD.nome");
            sql.AppendLine(", PROD.descricao");
            sql.AppendLine(", PROD.codCategoria");
            sql.AppendLine(", CAT.name AS nomeCategoria");
            sql.AppendLine(", PROD.codEstadoUso");
            sql.AppendLine(", USO.descricao AS nomeEstadoUso");
            sql.AppendLine(", PROD.codMarca");
            sql.AppendLine(", MARCA.nome AS nomeMarca");
            sql.AppendLine(", PROD.codTipo");
            sql.AppendLine(", TIPO.nome AS nomeTipo");
            sql.AppendLine(", PROD.codVendedor");
            sql.AppendLine(", U.name AS nomeVendedor");
            sql.AppendLine(", PROD.preco");
            sql.AppendLine(", PROD.quantidade");
            sql.AppendLine($"FROM {Product.table} PROD");
            sql.AppendLine("LEFT JOIN ProdutoCategoria CAT");
            sql.AppendLine("ON CAT.categoryId = PROD.codCategoria");
            sql.AppendLine("LEFT JOIN ProdutoEstadoUso USO");
            sql.AppendLine("ON USO.codEstadoUso = PROD.codEstadoUso");
            sql.AppendLine("LEFT JOIN ProdutoMarca MARCA");
            sql.AppendLine("ON MARCA.codMarca = PROD.codMarca");
            sql.AppendLine("LEFT JOIN ProdutoTipo TIPO");
            sql.AppendLine("ON TIPO.codTipo = PROD.codTipo");
            sql.AppendLine("LEFT JOIN AcessUser U");
            sql.AppendLine("ON U.userId = PROD.codVendedor");
            sql.AppendLine("WHERE 1 = 1");
            if (pf.ProductIds.Count > 0)
                {
                sql.AppendFormat("AND PROD.codProduto {0}", DbHelper.toSqlInInt(pf.ProductIds));
            }
            if (pf.CategoryIds.Count > 0)
            {
                sql.AppendFormat("AND PROD.codCategoria {0}", DbHelper.toSqlInInt(pf.CategoryIds));
            }
            if (pf.UseStateIds.Count > 0)
            {
                sql.AppendFormat("AND PROD.codEstadoUso {0}", DbHelper.toSqlInInt(pf.UseStateIds));
            }
            if (pf.BrandIds.Count > 0)
            {
                sql.AppendFormat("AND PROD.codMarca {0}", DbHelper.toSqlInInt(pf.BrandIds));
            }
            if (pf.TypeIds.Count > 0)
            {
                sql.AppendFormat("AND PROD.codTipo {0}", DbHelper.toSqlInInt(pf.TypeIds));
            }
            if (pf.SalesPerson.UserId + "" != string.Empty)
            {
                sql.AppendFormat("AND U.usuario {0}", pf.SalesPerson.UserId);
            }
            return sql.ToString();
        }

        public List<Product> GetProducts(ProductFilter prodFilter)
        {

            List<Product> produtos = new List<Product>();
            using (IDataReader dr = DbHelper.ReturnDataReader(GetSqlProducts(prodFilter), con))
            {
                while (dr.Read())
                {
                    produtos.Add(drToProd(dr));
                }
            }

            for (int i = 0; i < produtos.Count; i++)
            {
                produtos[i] = getimagesProduct(produtos[i]);
            }
            con.Disconnect();
            return produtos;
        }
        public Product GetProduct(int codProduto)
        {
            ProductFilter filtro = new ProductFilter();
            filtro.ProductIds.Add(codProduto);
            List<Product> exit = GetProducts(filtro);
            if (exit.Count > 0)
            {
                return exit.FirstOrDefault();
            }
            return new Product();
        }
    }
}

