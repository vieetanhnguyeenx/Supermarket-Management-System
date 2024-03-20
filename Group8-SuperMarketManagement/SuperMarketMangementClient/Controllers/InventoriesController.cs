using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace SuperMarketMangementClient.Controllers
{
    public class InventoriesController : Controller
    {


        // GET: Inventories
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }


        public IActionResult Delete()
        {
            return View();
        }

    }
}
