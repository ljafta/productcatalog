namespace ProductCatalog.Api.DTOs
{
    // Used for GET /api/categories/tree
    public record CategoryTreeDto(
        Guid Id,
        string Name,
        string? Description,
        List<CategoryTreeDto> Children
    );
}
