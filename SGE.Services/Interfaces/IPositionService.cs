using SGE.Shared.Common;
using SGE.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Services.Interfaces
{
    public interface IPositionService
    {
        Task<Result<List<PositionDto>>> GetAllAsync();
        Task<Result<PositionDto?>> GetByIdAsync(int id);
    }
}
