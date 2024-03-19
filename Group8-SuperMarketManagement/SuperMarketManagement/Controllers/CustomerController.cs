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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository = new CustomerRepository();
        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<CustomerDTORespone>> GetCustomers() => repository.GetCustomers();
        [HttpGet("{id}")]
        public ActionResult<CustomerDTORespone> GetCustomerById(int id) => repository.GetCustomerById(id);
        [HttpPost]
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
