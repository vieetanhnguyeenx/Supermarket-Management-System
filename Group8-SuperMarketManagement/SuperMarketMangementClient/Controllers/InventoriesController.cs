using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class InventoriesController : Controller
    {


        private readonly HttpClient client = null;
        public InventoriesController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

        }

        // GET: Inventories
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lst = new List<InventoryDTORespone>();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Inventory?$filter= inventoryID eq " + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            lst = JsonSerializer.Deserialize<List<InventoryDTORespone>>(strData, options);
            var inventoryDTORespone = lst[0];
            return View(inventoryDTORespone);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Quantity,PurchasePrice,EntryDate,EmployeeID")] InventoryDTOCreate customer)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5000/api/Inventory", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }
            return View(customer);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int InventoryID)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:5000/api/Inventory/" + InventoryID);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete");
        }
    }
}
