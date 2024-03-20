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
using System.Text.Json;

namespace SuperMarketMangementClient.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly HttpClient client = null;
        public SuppliersController()
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,Address,Phone")] SupplierDTOCreate customer)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5000/api/Supplier", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }
            return View(customer);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var sup = new SupplierDTORespone();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Supplier/"+id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            sup = JsonSerializer.Deserialize<SupplierDTORespone>(strData, options);
            return View(sup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("SupplierID,CompanyName,Address,Phone")] SupplierDTOPUT customer)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:5000/api/Supplier/" + customer.SupplierID, customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else return NotFound();
            }
            return View(customer);

        }
        public async Task<IActionResult> Delete(int id)
        {
            var sup = new SupplierDTORespone();
            HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/Supplier/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            sup = JsonSerializer.Deserialize<SupplierDTORespone>(strData, options);
            return View(sup);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int SupplierID)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:5000/api/Supplier/" + SupplierID);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete");
        }

    }
}
