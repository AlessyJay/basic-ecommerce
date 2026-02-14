namespace basic_ecommerce.Dto
{
    public class RegisterDto
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }

    public class LoginDto
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }

    public class MeDto
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTimeOffset joinedAt { get; set; }
        public DateTimeOffset updateAt { get; set; }
    }
}
