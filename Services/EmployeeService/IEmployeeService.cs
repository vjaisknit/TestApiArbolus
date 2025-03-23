using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ApiResponse<List<Employee>>> GetAllEmployeesAsync();
        Task<ApiResponse<List<Employee>>> GetOldestEmployeesAsync();
    }
}
