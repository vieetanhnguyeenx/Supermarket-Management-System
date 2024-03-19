using DataAccess.DTOs;
using DataAccess.Repository;
using DataAccess.Repository.Iplm;
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
            try
            {
                repository.SaveCategory(category);
                return NoContent();
            }catch (Exception ex) { 
                return BadRequest(ex.Message);  
            }
        }
        [HttpPut("{id}")]
        public IActionResult PutCategory( int id ,CategoryDTOPUT category)
        {
            var cate = repository.GetCategoryById(id);
            if (cate == null) 
                return NotFound(); 
            try
            {
                repository.UpdateCategory(category);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }  
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            var cate = repository.GetCategoryById(id);
            if (cate == null)
                return NotFound();
            try
            {
                repository.DeleteCategory(id);
                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
