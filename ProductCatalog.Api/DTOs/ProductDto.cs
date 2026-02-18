namespace ProductCatalog.Api.DTOs
{
    public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    int Quantity,
    Guid CategoryId
    );
}
