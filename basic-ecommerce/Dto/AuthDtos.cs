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
}
