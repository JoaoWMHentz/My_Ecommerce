using Helpers.connection;
using OpenEcommerceDLL.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.product
{
    public class ProductUseState
    {
        public static string table = "ProdutoEstadoUso";
        [KeyAutoIncrement]
        public int ProductUseStateId { get; set; }
        [Save]
        public string? Description { get; set; }

        Connection con = new Connection();

        public ProductUseState save(ProductUseState useState)
        {
            bool exists = useState.ProductUseStateId > 0;
            try
            {
                DbHelper.saveObject(con, useState, ProductUseState.table, exists);
            }
            catch (Exception)
            {
                throw;
            }
            if (!exists)
            {
                useState.ProductUseStateId = DbHelper.GetLastId(ProductUseState.table, "codEstadoUso", con);
            }
            return useState;
        }
        public ProductUseState getUseState(int id)
        {
            ProductUseState useState = new ProductUseState();
            useState.ProductUseStateId = id;
            try
            {
                return GetUseStates(useState).FirstOrDefault();
            }
            catch (Exception)
            {
                return useState;
            }
        }

        public List<ProductUseState> GetUseStates(ProductUseState useState)
        {
            List<ProductUseState> exit = new List<ProductUseState>();
            foreach (ProductUseState pc in DbHelper.getObject(con, useState, ProductUseState.table))
            {
                exit.Add(pc);
            }
            return exit;
        }

    }
}
