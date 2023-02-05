using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.product
{
    public class ProductBrand
    {
        public int BrandId { get; set; }
        public ProductCategory? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
