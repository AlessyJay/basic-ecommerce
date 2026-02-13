namespace basic_ecommerce.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string? refreshToken { get; set; }
        public DateTimeOffset? RefreshExpiresAt { get; set; }
        public DateTimeOffset joinedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset updatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
