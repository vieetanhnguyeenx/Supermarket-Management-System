using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using System.Net.Http.Headers;
using DataAccess.DTOs;
using System.Collections;
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
            var cust = new Customer();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Customer?$filter=customerID eq " + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            cust = JsonSerializer.Deserialize<Customer>(strData, options);
            return View(cust);    
        }

        
   
    }
}
