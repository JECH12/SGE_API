using SGE.Shared.Common;
using SGE.Shared.DTOs;

namespace SGE.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result<List<EmployeeDto>>> GetAllAsync();
        Task<Result<EmployeeDto?>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(CreateEmployeeDto dto);
        Task<Result<int>> UpdateAsync(int id, UpdateEmployeeDto dto);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<List<EmployeeDto>>> SearchAsync(string filter);
    }
}
