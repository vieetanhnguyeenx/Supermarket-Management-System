using DataAccess.Repository;
using DataAccess.Repository.Iplm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarketManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailServiceController : ControllerBase
    {
        private readonly IEmailService email = new EmailService();
        private readonly ICustomerRepository repository = new CustomerRepository(); 
        private readonly ISalesTransactionRepository transactionRepository = new SalesTransactionRepository();
        [HttpGet("{CustomerID}/{TransactionID}")]
        public  IActionResult sendmail(int CustomerID, int TransactionID)
        {
            var cus= repository.GetCustomerById(CustomerID);
            var ord = transactionRepository.GetTransaction(TransactionID);
            try
            {
                 email.Send("nghiatdhe163119@fpt.edu.vn", "xuankhbm2@gmail.com","Thank You","Thank You");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        
    }
}
