using DataAccess.Common;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient client = null;

        private readonly string JWTToken = "";
        private readonly string UserId = "";
        private readonly IServiceProvider _services;
        public CategoriesController(IServiceProvider services)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppRole1.Admin)]
        public async Task<IActionResult> Create([Bind("CategoryName,Description")] CategoryDTOCreateRequest customer)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5000/api/Category", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }
            return View(customer);

        }
        [Authorize(Roles = AppRole1.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var sup = new List<CategoryDTOResponse>();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Category?$filter=categoryID eq " + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            sup = JsonSerializer.Deserialize<List<CategoryDTOResponse>>(strData, options);
            var a = sup[0];
            return View(a);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppRole1.Admin)]
        public async Task<IActionResult> Edit([Bind("CategoryID,CategoryName,Description")] CategoryDTOPUT customer)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:5000/api/Category/" + customer.CategoryID, customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else return NotFound();
            }
            return View(customer);

        }
        [Authorize(Roles = AppRole1.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var sup = new List<CategoryDTOResponse>();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Category?$filter=categoryID eq " + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            sup = JsonSerializer.Deserialize<List<CategoryDTOResponse>>(strData, options);
            var a = sup[0];
            return View(a);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppRole1.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int CategoryID)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:5000/api/Category/" + CategoryID);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete");
        }
    }
}
