using DataAccess.DTOs;
using DataAccess.Repository;
using DataAccess.Repository.Iplm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace SuperMarketManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository = new CategoryRepository();
        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<CategoryDTOResponse>> GetCategories() => repository.GetCategories();
        [HttpPost]
        public IActionResult PostCategory(CategoryDTOCreateRequest category)
        {
            repository.SaveCategory(category);
        }
    }
}
