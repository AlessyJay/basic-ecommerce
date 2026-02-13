namespace basic_ecommerce.Dto
{
    public class Products
    {
        public required Guid userId { get; set; }
        public required string productName { get; set; }
        public required string productContent { get; set; }
        public required double price { get; set; }
    }

    public class UpdateProduct
    {
        public string? productName { get; set; }
        public string? productContent { get; set; }
        public double? price { get; set; }
    }

    public class DeleteProduct
    {
        public Guid productId { get; set; }
    }
}
