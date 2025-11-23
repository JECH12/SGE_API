using SGE.Shared.Common;
using SGE.Shared.DTOs;

namespace SGE.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Result<List<DepartmentDto>>> GetAllAsync();
        Task<Result<DepartmentDto?>> GetByIdAsync(int id);
    }
}
