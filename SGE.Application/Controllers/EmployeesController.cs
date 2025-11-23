using Microsoft.AspNetCore.Mvc;
using SGE.Services.Interfaces;
using SGE.Shared.DTOs;
using SGE.Shared.Common;
using SGE.Shared.Constans;

namespace SGE.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetAllAsync();
            return HandleResponse(result);
        }

        // GET: api/employees/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return HandleResponse(result);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.CreateAsync(dto);
            return HandleResponse(result);
        }

        // PUT: api/employees/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.UpdateAsync(id, dto);
            return HandleResponse(result);
        }

        // DELETE: api/employees/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteAsync(id);
            return HandleResponse(result);
        }
        private IActionResult HandleResponse<T>(Result<T> result)
        {
            if (result.Success) return Ok(result);

            if (result.Message == Messages.NotFound)
                return NotFound(result);

            return BadRequest(result);
        }
    }
}
