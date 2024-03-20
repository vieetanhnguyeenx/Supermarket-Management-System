using DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient client = null;
        public CustomersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var cust = new List<CustomerDTORespone>();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Customer?$filter=customerID eq " + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            cust = JsonSerializer.Deserialize<List<CustomerDTORespone>>(strData, options);
            return View(cust[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CustomerID,LastName,FirstName,Address,Phone,Point,Email")] CustomerDTOPUT customer)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:5000/api/Customer/" + customer.CustomerID, customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else return NotFound();
            }
            return View(customer);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,Address,Phone,Point,Email")] CustomerDTOCreate customer)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5000/api/Customer", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }
            return View(customer);

        }
    }
}
