using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EmployeeService;

namespace TestApiArbolus.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeService.GetAllEmployeesAsync();
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpGet("oldest")]
        public async Task<IActionResult> GetOldestEmployees()
        {
            var response = await _employeeService.GetOldestEmployeesAsync();
            return StatusCode((int)response.ResponseStatusCode, response);
        }
    }
}
