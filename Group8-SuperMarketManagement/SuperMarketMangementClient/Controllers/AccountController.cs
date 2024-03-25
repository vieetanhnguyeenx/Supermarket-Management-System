using DataAccess.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarketMangementClient.Controllers
{
	public class AccountController : Controller
	{

		public IActionResult Login()
		{
			return View();
		}
		[Authorize(Roles = AppRole1.Admin)]
		public IActionResult Register()
		{
			return View();
		}
	}
}
