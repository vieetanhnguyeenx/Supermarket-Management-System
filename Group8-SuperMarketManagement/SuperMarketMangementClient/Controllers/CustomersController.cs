﻿using DataAccess.Common;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient client = null;
        private readonly string JWTToken = "";
        private readonly string UserId = "";
        private readonly string UserRole = "";
        private readonly IServiceProvider _services;

        public CustomersController(IServiceProvider services)
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

        [Authorize(Roles = AppRole1.Admin + "," + AppRole1.Employee)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = AppRole1.Admin)]
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
        [Authorize(Roles = AppRole1.Admin)]
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
        [Authorize(Roles = AppRole1.Admin + "," + AppRole1.Employee)]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,Address,Phone,Point,Email")] CustomerDTOCreate customer)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5000/api/Customer", customer);
                if (response.IsSuccessStatusCode)
                {
                    if (UserRole.ToLower() == AppRole1.Employee.ToLower())
                    {
                        return RedirectToAction("Create", "Transaction");
                    }
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }
            return View(customer);

        }
    }
}
