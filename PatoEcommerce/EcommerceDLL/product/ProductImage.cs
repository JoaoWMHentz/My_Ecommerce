using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEcommerceDLL.product
{
    public class ProductImage
    {
        public static string table = "ProdutoImage";

        public int imageId { get; set; }
        public string Image { get; set; }
    }
}
