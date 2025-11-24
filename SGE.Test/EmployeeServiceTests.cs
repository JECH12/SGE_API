using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SGE.Data.Context;
using SGE.Data.Entities;
using SGE.Services.Services;
using SGE.Shared.DTOs;
using SGE.Shared.Constans;

namespace SGE.Test
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _service;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            SeedDatabase();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
                cfg.CreateMap<CreateEmployeeDto, Employee>();
                cfg.CreateMap<UpdateEmployeeDto, Employee>();
            });
            _mapper = config.CreateMapper();

            _service = new EmployeeService(_context, _mapper);
        }

        private void SeedDatabase()
        {
            var department = new Department { Id = 1, Name = "IT" };
            var position = new Position { Id = 1, Name = "Developer" };

            _context.Departments.Add(department);
            _context.Positions.Add(position);

            _context.Employees.Add(new Employee
            {
                Id = 1,
                FullName = "John Doe",
                Department = department,
                Position = position
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllEmployees()
        {
            var result = await _service.GetAllAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(1);
            result.Data.First().FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByIdExists()
        {
            var result = await _service.GetByIdAsync(1);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByIdNotExists()
        {
            var result = await _service.GetByIdAsync(999);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.EmployeeNotFound);
        }

        [Fact]
        public async Task CreateEmployee()
        {
            var dto = new CreateEmployeeDto
            {
                FullName = "Jane Smith",
                DepartmentId = 1,
                PositionId = 1
            };

            var result = await _service.CreateAsync(dto);

            result.Success.Should().BeTrue();
            result.Data.Should().BeGreaterThan(0);
            result.Message.Should().Be(Messages.EmployeeCreated);
        }

        [Fact]
        public async Task UpdateEmployeeExists()
        {
            var dto = new UpdateEmployeeDto
            {
                FullName = "John Updated",
                DepartmentId = 1,
                PositionId = 1
            };

            var result = await _service.UpdateAsync(1, dto);

            result.Success.Should().BeTrue();
            result.Data.Should().Be(1);
            result.Message.Should().Be(Messages.EmployeeUpdated);

            var updatedEmployee = await _context.Employees.FindAsync(1);
            updatedEmployee!.FullName.Should().Be("John Updated");
        }

        [Fact]
        public async Task UpdateNotExists()
        {
            var dto = new UpdateEmployeeDto
            {
                FullName = "Updated Name",
                DepartmentId = 1,
                PositionId = 1
            };

            var result = await _service.UpdateAsync(999, dto);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.EmployeeNotFound);
        }

        [Fact]
        public async Task DeleteExists()
        {
            var result = await _service.DeleteAsync(1);

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();
            result.Message.Should().Be(Messages.EmployeeDeleted);
        }

        [Fact]
        public async Task DeleteNotExists()
        {
            var result = await _service.DeleteAsync(999);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.NotFound);
        }
    }
}
