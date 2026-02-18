namespace ProductCatalog.Api.DTOs
{
    public record CreateProductDto(

        string Name,
        string? Description,
        string SKU,
        decimal Price,
        int Quantity,
        Guid CategoryId
    );

}
