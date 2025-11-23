using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGE.Data.Context;
using SGE.Services.Interfaces;
using SGE.Shared.Common;
using SGE.Shared.Constans;
using SGE.Shared.DTOs;

namespace SGE.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<DepartmentDto>>> GetAllAsync()
        {
            try
            {
                var departments = await _context.Departments
                    .AsNoTracking()
                    .ToListAsync();

                var dtos = _mapper.Map<List<DepartmentDto>>(departments);
                return Result<List<DepartmentDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<List<DepartmentDto>>.Fail($"{Messages.DepartmentsError} {ex.Message}");
            }
        }

        public async Task<Result<DepartmentDto?>> GetByIdAsync(int id)
        {
            try
            {
                var department = await _context.Departments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (department == null)
                    return Result<DepartmentDto?>.Fail(Messages.DepartmentNotFound);

                var dto = _mapper.Map<DepartmentDto>(department);
                return Result<DepartmentDto?>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<DepartmentDto?>.Fail($"{Messages.DepartmentError} {ex.Message}");
            }
        }
    }
}
