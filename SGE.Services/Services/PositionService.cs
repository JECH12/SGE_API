using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGE.Data.Context;
using SGE.Services.Interfaces;
using SGE.Shared.Common;
using SGE.Shared.Constans;
using SGE.Shared.DTOs;

namespace SGE.Services.Services
{
    public class PositionService : IPositionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PositionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<PositionDto>>> GetAllAsync()
        {
            try
            {
                var positions = await _context.Positions
                    .AsNoTracking()
                    .ToListAsync();

                var dtos = _mapper.Map<List<PositionDto>>(positions);
                return Result<List<PositionDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<List<PositionDto>>.Fail($"{Messages.PositionsError} {ex.Message}");
            }
        }

        public async Task<Result<PositionDto?>> GetByIdAsync(int id)
        {
            try
            {
                var position = await _context.Positions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (position == null)
                    return Result<PositionDto?>.Fail(Messages.PositionNotFound);

                var dto = _mapper.Map<PositionDto>(position);
                return Result<PositionDto?>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<PositionDto?>.Fail($"{Messages.PositionError} {ex.Message}");
            }
        }
    }
}
