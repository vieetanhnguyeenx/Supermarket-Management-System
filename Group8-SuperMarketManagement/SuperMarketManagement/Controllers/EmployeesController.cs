using DataAccess.Common;
using DataAccess.DTOs;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace SuperMarketManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository repository;
        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = AppRole.Admin)]
        public ActionResult<IEnumerable<EmployeeDTOResponse>> GetEmployees() => repository.GetEmployees();

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(string id, EmployeePutDTORequest employee)
        {
            var e = await repository.GetEmployeeById(id);
            if (e == null)
                return NotFound();
            try
            {
                repository.UpdateEmployee(id, employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/{roleName}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> ChangeRole(string id, string roleName)
        {
            var e = await repository.GetEmployeeById(id);
            if (e == null)
                return NotFound();
            try
            {
                await repository.UpdateEmployeeRole(id, roleName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var e = await repository.GetEmployeeById(id);
            if (e == null)
                return NotFound();
            try
            {
                repository.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> GetById(string id)
        {
            var e = await repository.GetEmployeeById(id);
            if (e == null)
                return NotFound();

            return Ok(e);
        }
    }
}
