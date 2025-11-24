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
    public class PositionServiceTests
    {
        private readonly PositionService _service;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PositionServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            SeedDatabase();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Position, PositionDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new PositionService(_context, _mapper);
        }

        private void SeedDatabase()
        {
            _context.Positions.AddRange(
                new Position { Id = 1, Name = "Developer" },
                new Position { Id = 2, Name = "Manager" }
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllPositions()
        {
            var result = await _service.GetAllAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Data.Select(p => p.Name).Should().Contain(new[] { "Developer", "Manager" });
        }

        [Fact]
        public async Task GetByIdExists()
        {
            var result = await _service.GetByIdAsync(1);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Name.Should().Be("Developer");
        }

        [Fact]
        public async Task GetByIdNotExists()
        {
            var result = await _service.GetByIdAsync(999);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.PositionNotFound);
        }
    }
}
