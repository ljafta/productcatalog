namespace ProductCatalog.Api.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // ParentId = null → root category
        public Guid? ParentCategoryId { get; set; }

        // Used only when building tree in memory
        public List<Category> Children { get; set; } = new();
    }
}
