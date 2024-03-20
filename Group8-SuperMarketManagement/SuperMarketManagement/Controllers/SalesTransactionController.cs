using DataAccess.DTOs;
using DataAccess.Repository.Iplm;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarketManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTransactionController : ControllerBase
    {
        private ISalesTransactionRepository repository = new SalesTransactionRepository();
        private IProductRepository productRepository = new ProductRepository();
        [HttpGet]
        public ActionResult<IEnumerable<SalesTransactionDTOResponse>> GetTransactions() => repository.GetAllTransactions();

        [HttpGet("Employee/{employeeID}")]
        public ActionResult<IEnumerable<SalesTransactionDTOResponse>> GetTransactionsByEmployeeID(string employeeID) => repository.GetAllTransactionsByEmployeeID(employeeID);

        [HttpGet("{id}")]
        public ActionResult<SalesTransactionDTOResponse> GetTransactionById(int id) => repository.GetTransaction(id);

        [HttpPost]
        public IActionResult PostProduct(SalesTransactionDTOPOST postTransaction)
        {
            repository.SaveTransaction(postTransaction);
            foreach(var x in postTransaction.TransactionDetails)
            {
                var productTmp = productRepository.GetProduct(x.ProductID);
                productTmp.TotalQuantity -= x.Quantity;
                productRepository.MinusProduct(productTmp);
            }
            return NoContent();
        }

        [HttpPut("Disable/{id}")]
        public IActionResult PutProduct(int id)
        {
            var ts = repository.GetTransaction(id);
            if (ts == null)
            {
                return NotFound();
            }
            repository.DisableTransaction(id);
            return NoContent();
        }
    }
}

