using CarParkSystem.App.DTOs;
using CarParkSystem.App.Interfaces;
using CarParkSystem.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubdivisionController : ControllerBase
    {
        private readonly ISubdivisionService _service;

        public SubdivisionController(ISubdivisionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubdivision([FromBody] CreateSubdivisionDto dto)
        {
            try
            {
                await _service.AddSubdivisionAsync(dto);
                return Ok("Subdivision created successfully.");
            }
            catch (ArgumentException ex)
            {
               // _logger.LogWarning("Validation error: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error creating user.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubdivision(Guid id)
        {
            try
            {
                var subdivision = await _service.GetSubdivisionByIdAsync(id);
                return Ok(subdivision);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 если нет
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubdivisions()
        {
            var subdivisions = await _service.GetAllSubdivisionsAsync();
            return Ok(subdivisions);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredBids(
           [FromQuery] string? name,
           [FromQuery] string? address,
           [FromQuery] string? phoneNumber,
           [FromQuery] string? status)
        {
            var subdivisions = await _service.GetFilteredSubdivisionsAsync(name, address, phoneNumber, status);
            return Ok(subdivisions);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubdivision(Guid id, [FromBody] SubdivisionDto dto)
        {
            try
            {
                await _service.UpdateSubdivisionAsync(id, dto);
                return Ok("Subdivision updated successfully.");
            }
            catch (ArgumentException ex)
            {
               // _logger.LogWarning("Validation error: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error updating user.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubdivision(Guid id)
        {
            try
            {
                await _service.DeleteSubdivisionAsync(id);
                return Ok("Subdivision deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 если нет
            }
        }
    }
}