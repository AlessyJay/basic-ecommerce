namespace basic_ecommerce.Dto
{
    public class RefreshTokenRequest
    {
        public Guid userId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
