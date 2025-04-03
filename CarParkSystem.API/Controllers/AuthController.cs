using CarParkSystem.App.DTOs;
using CarParkSystem.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
            if (user == null)
            {
                return Unauthorized("Неверный логин или пароль");
            }

            return Ok(user);
        }
    }
}