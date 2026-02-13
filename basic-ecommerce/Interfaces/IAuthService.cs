using basic_ecommerce.Dto;
using basic_ecommerce.Models;

namespace basic_ecommerce.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto req);
        Task<TokenResponse?> LoginAsync(LoginDto req);
        Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest req);
    }
}
