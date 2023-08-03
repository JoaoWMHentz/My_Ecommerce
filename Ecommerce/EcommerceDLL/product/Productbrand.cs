using Helpers.connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.product
{
    public class ProductBrand
    {
        public static string table = "ProdutoMarca";
        [Save]
        public int BrandId { get; set; }
        [Save]
        public string? Name { get; set; }
        [Save]
        public string? Description { get; set; }

        Connection con = new Connection();

        public ProductBrand save(ProductBrand brand)
        {
            bool exists = brand.BrandId > 0;
            try
            {
                DbHelper.saveObject(con, brand, ProductBrand.table, exists);
            }
            catch (Exception)
            {
                throw;
            }
            if (!exists)
            {
                brand.BrandId = DbHelper.GetLastId(ProductBrand.table, "codMarca", con);
            }
            return brand;
        }
        public ProductBrand getUseState(int id)
        {
            ProductBrand brand = new ProductBrand();
            brand.BrandId = id;
            try
            {
                return GetUseStates(brand).FirstOrDefault();
            }
            catch (Exception)
            {
                return brand;
            }
        }

        public List<ProductBrand> GetUseStates(ProductBrand brand)
        {
            List<ProductBrand> exit = new List<ProductBrand>();
            foreach (ProductBrand pc in DbHelper.getObject(con, brand, ProductBrand.table))
            {
                exit.Add(pc);
            }
            return exit;
        }
    }

}
