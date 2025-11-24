using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SGE.Data.Context;
using SGE.Data.Entities;
using SGE.Services.Services;
using SGE.Shared.Constans;
using SGE.Shared.DTOs;

namespace SGE.Test
{
    public class DepartmentServiceTests
    {
        private readonly DepartmentService _service;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            SeedDatabase();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Department, DepartmentDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new DepartmentService(_context, _mapper);
        }

        private void SeedDatabase()
        {
            _context.Departments.AddRange(
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "HR" }
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllDepartments()
        {
            var result = await _service.GetAllAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Data.Select(d => d.Name).Should().Contain(new[] { "IT", "HR" });
        }

        [Fact]
        public async Task GetByIdExists()
        {
            var result = await _service.GetByIdAsync(1);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Name.Should().Be("IT");
        }

        [Fact]
        public async Task GetByIdNotExists()
        {
            var result = await _service.GetByIdAsync(999);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.DepartmentNotFound);
        }
    }
}
