namespace ProductCatalog.Api.DTOs
{
    public record CategoryDto(
        Guid Id,
        string Name,
        string? Description,
        Guid? ParentCategoryId
    );
}
