using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.Repositories.Interfaces;

namespace ProductCatalog.Api.Search
{
    /// <summary>
    /// Handles product searching and caching results in memory
    /// </summary>
    public class ProductSearchEngine
    {
        private readonly IRepository<Product> _repository;

        // Simple in-memory cache
        // Key   → search parameters
        // Value → search result
        private readonly Dictionary<string, List<Product>> _cache = new();

        public ProductSearchEngine(IRepository<Product> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Searches products by name and optional category
        /// Uses caching to avoid repeated work
        /// </summary>
        public IEnumerable<Product> Search(string? query, Guid? categoryId)
        {
            // Build a unique cache key
            var cacheKey = $"{query ?? "ALL"}_{categoryId?.ToString() ?? "ALL"}";

            // ✅ Return cached result if available
            if (_cache.TryGetValue(cacheKey, out var cachedResult))
            {
                return cachedResult;
            }

            // ❌ Not cached → perform search
            var products = _repository.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(p =>
                    p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            var result = products.ToList();

            // ✅ Store result in cache
            _cache[cacheKey] = result;

            return result;
        }

        /// <summary>
        /// Clears cache when data changes
        /// Call this after create/update/delete
        /// </summary>
        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}

