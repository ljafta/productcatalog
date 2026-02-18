using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.DTOs;
using System.Linq;

namespace ProductCatalog.Api.Extensions
{
    /// <summary>
    /// Builds a hierarchical category tree from a flat list
    /// </summary>
    public static class CategoryTreeBuilder
    {
        public static List<CategoryTreeDto> BuildTree(IEnumerable<Category> categories)
        {
            // Convert categories to lookup by ParentCategoryId
            var lookup = categories.ToLookup(c => c.ParentCategoryId);

            // Recursive function to build children
            List<CategoryTreeDto> Build(Guid? parentId)
            {
                return lookup[parentId]
                    .Select(c => new CategoryTreeDto(
                        c.Id,
                        c.Name,
                        c.Description,
                        Build(c.Id)   // recursion happens here
                    ))
                    .ToList();
            }

            // Root categories have ParentCategoryId == null
            return Build(null);
        }
    }
}
