using DataAccess.Repository.Iplm;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using BusinessObject;
using DataAccess.DTOs;
using Microsoft.AspNetCore.OData.Query;

namespace SuperMarketManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository repository = new ProductRepository();
        
        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ProductDTOResponse>> GetProducts() => repository.GetProducts();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<ProductDTOResponse>> Search(string keyword) => repository.SearchProducts(keyword);

        [HttpGet("{id}")]
        public ActionResult<ProductDTOResponse> GetProductById(int id) => repository.GetProduct(id);

        [HttpPost]
        public IActionResult PostProduct(ProductDTOPOST postProduct)
        {
            if (repository.GetProducts().FirstOrDefault(f => f.ProductName.ToLower().Equals(postProduct.ProductName.ToLower())) != null)
            {
                return BadRequest();
            }
            repository.SaveProduct(postProduct);
            return NoContent();
        }

        [HttpPut("Disable/{id}")]
        public IActionResult DisableProduct(int id)
        {
            var product = repository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            repository.DisableProduct(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, ProductDTOPUT postProduct)
        {
            var productTmp = repository.GetProduct(id);
            if (productTmp == null)
            {
                return NotFound();
            }

            if (!productTmp.ProductName.ToLower().Equals(postProduct.ProductName.ToLower())
                && repository.GetProducts().FirstOrDefault(f => f.ProductName.ToLower().Equals(postProduct.ProductName.ToLower())) != null)
            {
                return BadRequest();
            }
            repository.UpdateProduct(postProduct);
            return NoContent();
        }
    }
}
