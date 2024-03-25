using DataAccess.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarketMangementClient.Controllers
{
    public class ProductController : Controller
    {
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Edit()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Delete()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Disabled()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
        public IActionResult Undisable()
        {
            return View();
        }
    }
}
