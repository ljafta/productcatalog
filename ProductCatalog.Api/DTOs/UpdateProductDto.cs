namespace ProductCatalog.Api.DTOs
{
    public record UpdateProductDto(
    Guid Id,
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    int Quantity,
    Guid CategoryId);
}
