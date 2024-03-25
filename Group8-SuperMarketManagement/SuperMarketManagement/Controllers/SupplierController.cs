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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository repository = new SupplierRepository();
        [HttpGet()]
        [EnableQuery]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Inventory)]
        public ActionResult<IEnumerable<SupplierDTORespone>> GetSuppliers() => repository.GetSuppliers();
        [HttpGet("{id}")]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Inventory)]
        public ActionResult<SupplierDTORespone> GetSupplierById(int id) => repository.GetSupplierById(id);
        [HttpPost()]
        [Authorize(Roles = AppRole.Admin + "," + AppRole.Inventory)]
        public IActionResult PostSupplier(SupplierDTOCreate supplier)
        {
            try
            {
                repository.SaveSupplier(supplier);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public IActionResult PutSupplier(int id, SupplierDTOPUT supplier)
        {
            var sup = repository.GetSupplierById(id);
            if (sup == null)
            {
                return NotFound();
            }
            try
            {
                repository.UpdateSupplier(supplier);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public IActionResult DeleteSupplier(int id)
        {
            var sup = repository.GetSupplierById(id);
            if (sup == null)
            {
                return NotFound();
            }
            try
            {
                repository.DeleteSupplier(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
