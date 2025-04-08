using CarParkSystem.App.DTOs;
using CarParkSystem.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _service;

        public BidController(IBidService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] CreateBidDto dto)
        {
            try
            {
                await _service.AddBidAsync(dto);
                return Ok("Bid created successfully.");
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
        public async Task<IActionResult> GetBid(Guid id)
        {
            try
            {
                var bid = await _service.GetBidByIdAsync(id);
                return Ok(bid);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 если нет
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBids([FromQuery] Guid? userId)
        {
            try
            {
                var bids = await _service.GetAllBidsAsync();

                if (userId.HasValue)
                {
                    bids = bids.Where(b => b.UserID == userId.Value);
                }

                return Ok(bids);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredBids(
        [FromQuery] Guid? userId,
        [FromQuery] string? status,
        [FromQuery] string? cargo,
        [FromQuery] string? from,
        [FromQuery] string? to,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
        {
            var bids = await _service.GetFilteredBidsAsync(userId, status, cargo, from, to, startDate, endDate);
            return Ok(bids);
        }

        [HttpGet("by-subdivision-chek-status/{subdivisionId}")]
        public async Task<IActionResult> GetActiveBidsBySubdivision(Guid subdivisionId)
        {
            var activeBids = await _service.GetAllBidsByFilterAsync(
                b => b.SubdivisionID == subdivisionId &&
                     (b.Status == "На рассмотрении" || b.Status == "В работе")
            );

            return Ok(activeBids);
        }

        [HttpGet("by-subdivision/{subdivisionId}")]
        public async Task<IActionResult> GetBidsBySubdivision(Guid subdivisionId)
        {
            var activeBids = await _service.GetAllBidsByFilterAsync(
                b => b.SubdivisionID == subdivisionId
            );

            return Ok(activeBids);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(Guid id, [FromBody] BidDto dto)
        {
            try
            {
                await _service.UpdateBidAsync(id, dto);
                return Ok("Bid updated successfully.");
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
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            try
            {
                await _service.DeleteBidAsync(id);
                return Ok("Bid deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 если нет
            }
        }
    }
}
