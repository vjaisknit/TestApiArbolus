using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApiService
{
    public interface IEmployeeApiService
    {
        Task<ApiResponse<List<Employee>>> FetchEmployeesAsync();
    }
}
