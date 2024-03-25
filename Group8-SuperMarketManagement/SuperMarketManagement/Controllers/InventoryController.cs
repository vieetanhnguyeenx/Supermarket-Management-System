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
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository repository = new InventoryRepository();
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = AppRole.Admin)]
        public ActionResult<IEnumerable<InventoryDTORespone>> GetInventories() => repository.GetInventories();

        [HttpGet("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public ActionResult<InventoryDTORespone> GetInventoryById(int id) => repository.GetInventoryById(id);


        [HttpPost]
        [Authorize(Roles = AppRole.Inventory)]
        [Authorize(Roles = AppRole.Admin)]
        public IActionResult PostInventory(InventoryDTOCreate inventory)
        {
            try
            {
                repository.SaveInventory(inventory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public IActionResult Delete(int id)
        {

            var inv = repository.GetInventoryById(id);
            if (inv == null)
            {
                return NotFound();
            }
            try
            {
                repository.DeleteInventory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
