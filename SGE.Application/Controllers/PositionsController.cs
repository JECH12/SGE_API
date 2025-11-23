using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGE.Services.Interfaces;

namespace SGE.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: api/positions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _positionService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/positions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _positionService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
