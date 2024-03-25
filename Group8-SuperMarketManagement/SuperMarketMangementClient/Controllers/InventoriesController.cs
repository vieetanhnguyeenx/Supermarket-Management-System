using DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly HttpClient client = null;

        private readonly string JWTToken = "";
        private readonly string UserId = "";
        private readonly IServiceProvider _services;
        public InventoriesController(IServiceProvider services)
        {
            _services = services;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTToken);
            ISession session = _services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            JWTToken = session.GetString("JWToken") ?? "";
            UserId = session.GetString("UserId") ?? "";
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
