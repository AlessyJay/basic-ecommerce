using basic_ecommerce.Dto;
using basic_ecommerce.Interfaces;
using basic_ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace basic_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto req)
        {
            var user = await authService.RegisterAsync(req);

            if (user is null) return BadRequest("Account already exists!");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDto req)
        {
            var result = await authService.LoginAsync(req);

            if (result is null) return BadRequest("Invalid email or password!");

            return Ok(result);
        }

        [HttpPost("refresh-tokens")]
        public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest req)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken is null) return Unauthorized();

            var result = await authService.RefreshTokenAsync(req, refreshToken);

            if (result is null) return Unauthorized();

            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<MeDto>> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null) return Unauthorized();

            var result = await authService.GetMyInfo(Guid.Parse(userId));

            if (result is null) return NotFound();

            return Ok(result);
        }
    }
}
