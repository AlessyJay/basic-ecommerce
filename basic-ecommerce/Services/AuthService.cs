using basic_ecommerce.Database;
using basic_ecommerce.Dto;
using basic_ecommerce.Interfaces;
using basic_ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace basic_ecommerce.Services
{
    public class AuthService(AppDbContext context, IConfiguration config, IHttpContextAccessor httpContext) : IAuthService
    {
        private string CreateToken(User req)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, req.email),
                new Claim(ClaimTypes.NameIdentifier, req.Id.ToString()),
                new Claim(ClaimTypes.Name, req.firstName),
                new Claim(ClaimTypes.Surname, req.lastName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: config.GetValue<string>("AppSettings:Issuer")!,
                audience: config.GetValue<string>("AppSettings:Audience")!,
                signingCredentials: creds,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5)
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var random = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);

            return Convert.ToBase64String(random);
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };

            httpContext.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User req)
        {
            var refreshToken = GenerateRefreshToken();
            req.refreshToken = refreshToken;
            req.RefreshExpiresAt = DateTimeOffset.UtcNow.AddDays(30);

            await context.SaveChangesAsync();

            SetRefreshTokenCookie(refreshToken);

            return refreshToken;
        }

        private async Task<User> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.users.FindAsync(userId);

            if (user is null || user.refreshToken != refreshToken || user.RefreshExpiresAt <= DateTimeOffset.UtcNow)
            {
                return null!;
            }

            return user;
        }

        private async Task<TokenResponse> CreateTokenResponse(User user)
        {
            return new TokenResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<TokenResponse?> LoginAsync(LoginDto req)
        {
            var user = await context.users.FirstOrDefaultAsync(u => u.email == req.email);

            if (user == null) return null!;

            return await CreateTokenResponse(user);
        }

        public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken)
        {
            var userId = await context.users.FirstOrDefaultAsync(t => t.refreshToken == refreshToken);

            var user = await ValidateRefreshTokenAsync(userId!.Id, refreshToken);

            if (user is null)
            {
                return null!;
            }

            return await CreateTokenResponse(user);
        }

        public async Task<User?> RegisterAsync(RegisterDto req)
        {
            if (await context.users.AnyAsync(u => u.email == req.email)) return null!;

            var user = new User();

            var hashPassword = new PasswordHasher<User>().HashPassword(user, req.password);

            user.email = req.email;
            user.password = hashPassword;
            user.firstName = req.firstName;
            user.lastName = req.lastName;

            context.users.Add(user);

            await context.SaveChangesAsync();

            return user;
        }

        public async Task<MeDto?> GetMyInfo(Guid userId)
        {
            var user = await context.users.FindAsync(userId);

            if (user is null) return null;

            return new MeDto
            {
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                joinedAt = user.joinedAt,
                updateAt = user.updatedAt
            };
        }
    }
}
