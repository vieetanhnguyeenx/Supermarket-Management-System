using DataAccess.DTOs;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace SuperMarketManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IEmployeeRepository repository;
		public AccountsController(IEmployeeRepository repository)
		{
			this.repository = repository;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(EmployeeSignUpModel signUpRequest)
		{
			var result = await repository.SigUpAsyn(signUpRequest);
			if (result.Succeeded)
				return Ok(result.Succeeded);

			var errors = result.Errors;
			var message = string.Join(", ", errors.Select(x => "Code " + x.Code + " Description" + x.Description));
			return StatusCode(500, message);
		}

		[HttpPost("SignIn")]
		public async Task<IActionResult> SignIn(EmployeeSignInModel model)
		{
			var result = await repository.SignInAsyn(model);
			if (result != null)
				return Unauthorized();

			return Ok(result);
		}
	}
}
