using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarketMangementClient.Models;

namespace SuperMarketMangementClient.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MyDBContext _context;

        public DashboardController(MyDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? startDate, string? endDate)
        {
            List<Chart> charts = new List<Chart>();
            DateTime defaultStartDate = DateTime.Now.AddDays(-7);
            DateTime defaultEndDate = DateTime.Now;
            DateTime StartDate;
            DateTime EndDate; 
            if (startDate == null  && endDate == null)
            {
                StartDate = defaultStartDate;
                EndDate = defaultEndDate;
            }
            else
            {
                StartDate = DateTime.Parse(startDate);  
                EndDate = DateTime.Parse(endDate);  
            }
            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                decimal totalPurchasePrice = _context.Inventories
                .Where(i => i.EntryDate.Date == date.Date)
                .Sum(i => i.PurchasePrice);
                decimal totalPrice = _context.SalesTransactions
                .Where(i => i.TransactionDate == date.Date)
                .Sum(i => i.TotalPrice);

                double outCome = Convert.ToDouble(totalPurchasePrice);
                double inCome = Convert.ToDouble(totalPrice);
                Chart chart = new Chart { Date = date.ToString("yyyy-MM-dd"), Income = inCome, Outcome = outCome };
                charts.Add(chart);
            }
            return View(charts);
        }
    }
}