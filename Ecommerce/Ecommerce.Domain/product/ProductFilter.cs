using OpenEcommerceDLL.user;
using System.Collections.Generic;

namespace OpenEcommerceDLL.product
{
    public class ProductFilter
    {
        public ProductFilter()
        {
            ProductIds = new List<int>();
            CategoryIds = new List<int>();
            TypeIds = new List<int>();
            BrandIds = new List<int>();
            UseStateIds = new List<int>();
            SalesPerson = new User();
        }
        public List<int> ProductIds { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> BrandIds { get; set; }
        public List<int> UseStateIds { get; set; }
        public User SalesPerson { get; set; }
    }
}
