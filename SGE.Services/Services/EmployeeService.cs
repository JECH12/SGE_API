using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGE.Data.Context;
using SGE.Data.Entities;
using SGE.Services.Interfaces;
using SGE.Shared.Common;
using SGE.Shared.DTOs;
using SGE.Shared.Constans;

namespace SGE.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeeDto>>> GetAllAsync()
        {
            try
            {
                List<Employee> entities = await _context.Employees
                    .AsNoTracking()
                    .Include(e => e.Department)
                    .Include(e => e.Position)
                    .ToListAsync();

                List<EmployeeDto> dtos = _mapper.Map<List<EmployeeDto>>(entities);
                return Result<List<EmployeeDto>>.Ok(dtos);
            }

            catch (Exception ex)
            {
                return Result<List<EmployeeDto>>.Fail($"{Messages.EmployeesError} {ex.Message}");
            }
        }

        public async Task<Result<EmployeeDto?>> GetByIdAsync(int id)
        {
            try
            {
                Employee? entity = await _context.Employees
                    .AsNoTracking()
                    .Include(e => e.Department)
                    .Include(e => e.Position)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (entity == null)
                    return Result<EmployeeDto?>.Fail(Messages.EmployeeNotFound);

                EmployeeDto dto = _mapper.Map<EmployeeDto>(entity);
                return Result<EmployeeDto?>.Ok(dto);
            }

            catch (Exception ex)
            {
                return Result<EmployeeDto?>.Fail($"{Messages.EmployeeError}: {ex.Message}");
            }
        }

        public async Task<Result<int>> CreateAsync(CreateEmployeeDto dto)
        {
            try
            {
                if (!await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId))
                    return Result<int>.Fail(Messages.DepartmentNotFound);

                if (!await _context.Positions.AnyAsync(p => p.Id == dto.PositionId))
                    return Result<int>.Fail(Messages.PositionNotFound);

                Employee entity = _mapper.Map<Employee>(dto);

                _context.Employees.Add(entity);
                await _context.SaveChangesAsync();

                return Result<int>.Ok(entity.Id, Messages.EmployeeCreated);
            }

            catch (Exception ex)
            {
                return Result<int>.Fail($"{Messages.EmployeeCreateError} {ex.Message}");
            }
        }

        public async Task<Result<int>> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            try
            {
                Employee? entity = await _context.Employees.FindAsync(id);

                if (entity == null)
                    return Result<int>.Fail(Messages.EmployeeNotFound);

                if (!await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId))
                    return Result<int>.Fail(Messages.DepartmentNotFound);

                if (!await _context.Positions.AnyAsync(p => p.Id == dto.PositionId))
                    return Result<int>.Fail(Messages.PositionNotFound);

                _mapper.Map(dto, entity);

                _context.Employees.Update(entity);
                await _context.SaveChangesAsync();

                return Result<int>.Ok(entity.Id, Messages.EmployeeUpdated);
            }

            catch (Exception ex)
            {
                return Result<int>.Fail($"{Messages.EmployeeUpdateError} {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                Employee? entity = await _context.Employees.FindAsync(id);

                if (entity == null)
                    return Result<bool>.Fail(Messages.NotFound);

                _context.Employees.Remove(entity);
                await _context.SaveChangesAsync();

                return Result<bool>.Ok(true, Messages.EmployeeDeleted);
            }

            catch (Exception ex)
            {
                return Result<bool>.Fail($"{Messages.EmployeeDeleteError} {ex.Message}");
            }
        }

        public async Task<Result<List<EmployeeDto>>> SearchAsync(string filter)
        {
            try
            {
                filter = filter?.Trim().ToLower() ?? "";

                List<Employee> list = await _context.Employees
                    .AsNoTracking()
                    .Include(e => e.Department)
                    .Include(e => e.Position)
                    .Where(e =>
                        e.FullName.ToLower().Contains(filter) ||
                        e.Id.ToString() == filter)
                    .ToListAsync();

                List<EmployeeDto> dtos = _mapper.Map<List<EmployeeDto>>(list);

                return Result<List<EmployeeDto>>.Ok(dtos);
            }

            catch (Exception ex)
            {
                return Result<List<EmployeeDto>>.Fail($"{Messages.EmployeesError} {ex.Message}");
            }
        }
    }
}
