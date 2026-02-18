using ProductCatalog.Api.Domain.Entities;

namespace ProductCatalog.Api.Extensions
{
    // Static class because LINQ extensions must be static
    public static class ProductLinqExtensions
    {
        // Extension method to filter products by name
        // Usage:
        // products.FilterByName("laptop")
        public static IEnumerable<Product> FilterByName(
            this IEnumerable<Product> source,
            string searchTerm)
        {
            // If no search term is provided, return everything
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;

            // Case-insensitive name matching
            return source.Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        // Filters products by category
        // If categoryId is null → no filtering
        public static IEnumerable<Product> FilterByCategory(
            this IEnumerable<Product> source,
            Guid? categoryId)
        {
            if (categoryId is null)
                return source;

            return source.Where(p => p.CategoryId == categoryId);
        }

        // Returns only products that are in stock
        public static IEnumerable<Product> FilterInStock(
            this IEnumerable<Product> source)
        {
            return source.Where(p => p.Quantity > 0);
        }
    }
}
