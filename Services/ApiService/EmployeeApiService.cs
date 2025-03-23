using BusinessModel;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.ApiService
{
    public class EmployeeApiService : IEmployeeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmployeeApiService> _logger;
        private const string EmployeeApiUrl = "https://dummy.restapiexample.com/api/v1/employees";

        public EmployeeApiService(HttpClient httpClient, ILogger<EmployeeApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<List<Employee>>> FetchEmployeesAsync()
        {
            try
            {
                _logger.LogInformation("Calling Employee API: {ApiUrl}", EmployeeApiUrl);
                var response = await _httpClient.GetAsync(EmployeeApiUrl);
                var responseStatusCode = response.StatusCode;

                if (responseStatusCode == HttpStatusCode.TooManyRequests)
                {
                    _logger.LogWarning("API Rate Limit Exceeded (429 Too Many Requests).");
                    return new ApiResponse<List<Employee>>("Failure", "API Rate Limit Exceeded. Try again later.", null, responseStatusCode);
                }

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API error: {responseStatusCode}");
                    return new ApiResponse<List<Employee>>("Failure", $"API error: {responseStatusCode}", null, responseStatusCode);
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ApiResponse<List<Employee>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null || result.Data == null || result.Data.Count == 0)
                {
                    return new ApiResponse<List<Employee>>("Failure", "No employee data available.", null, responseStatusCode);
                }

                return new ApiResponse<List<Employee>>("Success", "Employees fetched successfully.", result.Data, responseStatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data from Employee API.");
                return new ApiResponse<List<Employee>>("Failure", "An internal error occurred.", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
