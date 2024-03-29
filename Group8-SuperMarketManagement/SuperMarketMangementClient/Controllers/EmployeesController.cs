﻿using DataAccess.Common;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HttpClient client = null;

        private readonly string JWTToken = "";
        private readonly string UserId = "";
        private readonly string UserRole = "";
        private readonly IServiceProvider _services;

        public EmployeesController(IServiceProvider services)
        {
            _services = services;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            ISession session = _services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            JWTToken = session.GetString("JWToken") ?? "";
            UserId = session.GetString("UserId") ?? "";
            UserRole = session.GetString("UserRole") ?? "";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTToken);

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

        [Authorize(Roles = AppRole1.Admin)]
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
        [Authorize(Roles = AppRole1.Admin)]
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

        [Authorize(Roles = AppRole1.Admin)]
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
        [Authorize(Roles = AppRole1.Admin)]
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
