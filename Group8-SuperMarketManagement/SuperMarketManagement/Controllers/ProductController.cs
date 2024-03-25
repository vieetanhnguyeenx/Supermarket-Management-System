using DataAccess.Common;
using DataAccess.DTOs;
using DataAccess.Repository;
using DataAccess.Repository.Iplm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public ActionResult<IEnumerable<ProductDTOResponse>> GetProducts() => repository.GetProducts();

        [HttpGet("Disabled")]
        [EnableQuery]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public ActionResult<IEnumerable<ProductDTOResponse>> GetDisabledProducts() => repository.GetDisabledProducts();

        [HttpGet("Disabled/{id}")]
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public ActionResult<ProductDTOResponse> GetDisanbledProductById(int id) => repository.GetDisabledProduct(id);

        [HttpGet("Search/{keyword}")]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public ActionResult<IEnumerable<ProductDTOResponse>> Search(string keyword) => repository.SearchProducts(keyword);
        [HttpGet("Disable/Search/{keyword}")]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public ActionResult<IEnumerable<ProductDTOResponse>> SearchDisable(string keyword) => repository.SearchDisableProducts(keyword);

        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public ActionResult<ProductDTOResponse> GetProductById(int id) => repository.GetProduct(id);

        [HttpPost]
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee + "," + AppRole.Inventory)]
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
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
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

        [HttpPut("Undisable/{id}")]
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public IActionResult UndisableProduct(int id)
        {
            var product = repository.GetDisabledProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            repository.UndisableProduct(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
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

        [HttpPut("{id}/{qty}")]
        [Authorize]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Employee)]
        public IActionResult PutProduct(int id, int qty)
        {
            var productTmp = repository.GetProduct(id);
            if (productTmp == null)
            {
                return NotFound();
            }
            productTmp.TotalQuantity -= qty;
            repository.MinusProduct(productTmp);
            return NoContent();
        }
    }
}
