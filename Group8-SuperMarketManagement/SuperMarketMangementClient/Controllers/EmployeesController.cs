using DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly HttpClient client = null;
        public EmployeesController()
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

        public async Task<IActionResult> Edit(string? id)
        {
            var cust = new EmployeeDTOResponse();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Employees/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            cust = JsonSerializer.Deserialize<EmployeeDTOResponse>(strData, options);
            return View(cust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,LastName,FirstName,DoB,HireDate,Address,Phone,RoleName")] EmployeeDTOResponse employee)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:5000/api/Employees/" + employee.Id, employee);
                if (response.IsSuccessStatusCode)
                {
                    HttpResponseMessage response1 = await client.GetAsync("https://localhost:5000/api/Employees/" + employee.Id + "/" + employee.RoleName);
                    if (response1.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else return NotFound();
            }
            return View(employee);

        }

        public async Task<IActionResult> Delete(string? id)
        {
            var cust = new List<EmployeeDTOResponse>();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Employees?$filter=id eq '" + id + "'");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            cust = JsonSerializer.Deserialize<List<EmployeeDTOResponse>>(strData, options);
            return View(cust[0]);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? Id)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:5000/api/Employees/" + Id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete");
        }
    }
}
