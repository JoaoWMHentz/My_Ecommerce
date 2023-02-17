using Helpers.connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.product
{
    public class ProductType
    {
        public static string table = "ProdutoTipo";
        [KeyAutoIncrement]
        public int TypeId { get; set; }
        [Save]
        public int CategoryId { get; set; }
        [Save]
        public string Name { get; set; }
        [Save]
        public string Description { get; set; }

        Connection con = new Connection();

        public ProductType save(ProductType type)
        {
            bool exists = type.TypeId > 0;
            try
            {
                DbHelper.saveObject(con, type, ProductType.table, exists);
            }
            catch (Exception)
            {
                throw;
            }
            if (!exists)
            {
                type.TypeId = DbHelper.GetLastId(ProductType.table, "codTipo", con);
            }
            return type;
        }
        public ProductType getProductType(int id)
        {
            ProductType type = new ProductType();
            type.TypeId = id;
            try
            {
                return GetProductTypes(type).FirstOrDefault();
            }
            catch (Exception)
            {
                return type;
            }
        }

        public List<ProductType> GetProductTypes(ProductType useState)
        {
            List<ProductType> exit = new List<ProductType>();
            foreach (ProductType pc in DbHelper.getObject(con, useState, ProductType.table))
            {
                exit.Add(pc);
            }
            return exit;
        }
    }
}
