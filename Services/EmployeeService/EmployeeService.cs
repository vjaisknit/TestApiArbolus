using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.ApiService;

namespace Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeApiService _employeeApiClient;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeApiService employeeApiClient, ILogger<EmployeeService> logger)
        {
            _employeeApiClient = employeeApiClient;
            _logger = logger;
        }

        public async Task<ApiResponse<List<Employee>>> GetAllEmployeesAsync()
        {
            _logger.LogInformation("Retrieving all employees...");
            var response = await _employeeApiClient.FetchEmployeesAsync();

            if (response.ResponseStatusCode != HttpStatusCode.OK || response.Data == null || response.Data.Count == 0)
            {
                _logger.LogWarning(response.Message);
                return response; 
            }

            return response; 
        }

        public async Task<ApiResponse<List<Employee>>> GetOldestEmployeesAsync()
        {
            _logger.LogInformation("Retrieving oldest employees...");
            var response = await _employeeApiClient.FetchEmployeesAsync();

            if (response.ResponseStatusCode != HttpStatusCode.OK || response.Data == null || response.Data.Count == 0)
            {
                _logger.LogWarning(response.Message);
                return response; 
            }

            var oldestEmployees = response.Data
            .GroupBy(e => e.EmployeeAge)
            .OrderByDescending(g => g.Key)
            .First()
            .ToList();
            return new ApiResponse<List<Employee>>(response.Status, "Oldest employees fetched successfully.", oldestEmployees, response.ResponseStatusCode);
        }
    }
}
