using Microsoft.AspNetCore.Mvc;

namespace SuperMarketMangementClient.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
