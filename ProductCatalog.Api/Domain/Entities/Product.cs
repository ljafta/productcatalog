namespace ProductCatalog.Api.Domain.Entities
{
    public class Product : IComparable<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string SKU { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CompareTo(Product? other)
        {
            if (other is null) return 1;
            return Price.CompareTo(other.Price);
        }
    }
}
