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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository repository = new SupplierRepository();
        [HttpGet()]
        [EnableQuery]
        public ActionResult<IEnumerable<SupplierDTORespone>> GetSuppliers() => repository.GetSuppliers();
        [HttpGet("{id}")]
        public ActionResult<SupplierDTORespone> GetSupplierById(int id) => repository.GetSupplierById(id);
        [HttpPost()]
        public IActionResult PostSupplier(SupplierDTOCreate supplier)
        {
            try
            {
                repository.SaveSupplier(supplier);
                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpPut("{id}")]
        public IActionResult PutSupplier(int id , SupplierDTOPUT supplier)
        {
            var sup = repository.GetSupplierById(id);
            if(sup == null)
            {
                return NotFound();
            }
            try
            {
                repository.UpdateSupplier(supplier);
                return NoContent(); 
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var sup = repository.GetSupplierById(id);
            if(sup == null)
            {
                return NotFound();  
            }
            try
            {
                repository.DeleteSupplier(id);  
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
