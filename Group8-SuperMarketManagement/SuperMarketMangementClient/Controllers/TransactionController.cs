using DataAccess.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarketMangementClient.Controllers
{
    public class TransactionController : Controller
    {
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Detail()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin + "," + AppRole1.Employee)]
        public IActionResult Create()
        {
            return View();
        }
    }
}
