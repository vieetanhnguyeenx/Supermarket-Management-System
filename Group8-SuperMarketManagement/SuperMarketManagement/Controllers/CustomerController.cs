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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository = new CustomerRepository();
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = AppRole.Admin)]
        public ActionResult<IEnumerable<CustomerDTORespone>> GetCustomers() => repository.GetCustomers();

        [HttpGet("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public ActionResult<CustomerDTORespone> GetCustomerById(int id) => repository.GetCustomerById(id);

        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        [Authorize(Roles = AppRole.Employee)]
        public IActionResult PostCustomer(CustomerDTOCreate customer)
        {
            try
            {
                repository.SaveCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        [Authorize(Roles = AppRole.Employee)]
        public IActionResult PutCustomer(int id, CustomerDTOPUT customer)
        {

            var cus = repository.GetCustomerById(id);
            if (cus == null)
            {
                return NotFound();
            }
            try
            {
                repository.UpdateCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
