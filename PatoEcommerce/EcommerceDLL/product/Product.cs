using OpenEcommerceDLL.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.product
{
    public class Product
    {
        public static readonly string table = "ProdutoCapa";

        public Product()
        {
            
            this.SalesPerson = new User();
            this.Category = new ProductCategory();
            this.Brand = new ProductBrand();
            this.Type = new ProductType();
            this.State = new ProductUseState();
            this.Images = new List<ProductImage>();
        }

        public int? ProductId { get; set; }

        public User SalesPerson { get; set; }

        public ProductCategory Category { get; set; }

        public ProductBrand Brand { get; set; }

        public ProductType Type { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public ProductUseState State { get; set; }

        public List<ProductImage> Images { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }
    }
}
