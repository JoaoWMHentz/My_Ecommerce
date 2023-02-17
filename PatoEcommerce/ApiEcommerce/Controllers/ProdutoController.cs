using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenEcommerceDLL.product;
using System.Data;

namespace WebBiblioteca_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("getProduct")]
        [Authorize(Roles = "Admin")]
        public List<Product> Get()
        {
            ProductDao Dao = new ProductDao();
            var filtro = new ProductFilter();
            return Dao.GetProducts(filtro);
        }

        [HttpPost]
        [Route("postProduct")]
        [Authorize(Roles = "Admin")]
        public Product Create([FromBody] Product produto)
        {
            ProductDao Dao = new ProductDao();
            return Dao.Save(produto);
        }
        
    }
}
