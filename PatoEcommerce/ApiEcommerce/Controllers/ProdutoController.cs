using Microsoft.AspNetCore.Mvc;
using OpenEcommerceDLL.product;
namespace WebBiblioteca_Api
{

    public class ProdutoController : Controller
    {
        [Route("api/produto/get")]
        [HttpGet()]
        public List<Product> Get()
        {
            ProductDao Dao = new ProductDao();
            var filtro = new ProductFilter();
            return Dao.GetProducts(filtro);
        }
        [Route("api/produto/post")]
        [HttpPost]
        public Product Create([FromBody] Product produto)
        {
            ProductDao Dao = new ProductDao();
            return Dao.Save(produto);
        }
        
    }
}
