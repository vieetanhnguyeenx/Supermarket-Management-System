using Microsoft.AspNetCore.Mvc;

namespace SuperMarketMangementClient.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Login()
		{
			foreach (var cookie in Request.Cookies.Keys)
			{
				Response.Cookies.Delete(cookie);
			}
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}
	}
}
