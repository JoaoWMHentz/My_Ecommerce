using Helpers.connection;

namespace OpenEcommerceDLL.product
{
    public class ProductCategory
    {
        public static string table = "ProdutoCategoria";
        [KeyAutoIncrement]
        public int CategoryId { get; set; }
        [Save]
        public string? Name { get; set; }
        [Save]
        public string? Description { get; set; }
        [Save]
        public string? Image { get; set; }

        public bool SubCat { get; set; }

        Connection con = new Connection();
        public ProductCategory salvar(ProductCategory category)
        {
            bool existe = category.CategoryId > 0;
            try
            {
                DbHelper.saveObject(con, category, ProductCategory.table, existe);
            }
            catch (Exception)
            {
                throw;
            }
            if (!existe)
            {
                category.CategoryId = DbHelper.GetLastId(ProductCategory.table, "categoryId", con);
            }
            return category;
        }
        public ProductCategory getCategory(int id)
        {
            ProductCategory category = new ProductCategory();
            category.CategoryId = id;
            return (ProductCategory)DbHelper.getObject(con, category, ProductCategory.table).FirstOrDefault();
        }

        public List<ProductCategory> GetCategories(ProductCategory category)
        {
            List<ProductCategory> exit = new List<ProductCategory>();
            foreach (ProductCategory pc in DbHelper.getObject(con, category, ProductCategory.table))
            {
                exit.Add(pc);
            }
            return exit;
        }
    }
}
