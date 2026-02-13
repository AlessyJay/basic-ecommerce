namespace basic_ecommerce.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid userId { get; set; }
        public string productName { get; set; } = string.Empty;
        public string productContent { get; set; } = string.Empty;
        public double price { get; set; } = 0;
        public DateTimeOffset createdAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset updatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
