using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.DTOs;
using ProductCatalog.Api.Extensions;
using ProductCatalog.Api.Repositories.InMemory;
using ProductCatalog.Api.Repositories.Interfaces;
using ProductCatalog.Api.Search;
using System.Text.Json;

namespace ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repository;
        private readonly ProductSearchEngine _searchEngine;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IRepository<Product> repository, ProductSearchEngine searchEngine, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _searchEngine = searchEngine;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/products
        /// Supports:
        /// - pagination
        /// - filtering by category
        /// - searching by product name
        /// </summary>
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? categoryId = null,
            [FromQuery] string? search = null)
        {
            // Get all products from repository
            var products = _repository.GetAll()

                // 🔹 Use YOUR custom LINQ extensions
                .FilterByCategory(categoryId)
                .FilterByName(search ?? string.Empty)
                .FilterInStock()

                // Convert to IQueryable only AFTER filtering
                .AsQueryable();

            var totalCount = products.Count();

            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.SKU,
                    p.Price,
                    p.Quantity,
                    p.CategoryId
                ))
                .ToList();

            return Ok(new
            {
                page,
                pageSize,
                totalCount,
                items = pagedProducts
            });
        }


        /// <summary>
        /// GET /api/products/{id}
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            _logger.LogInformation("Fetching product with id {ProductId}", id);

            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.SKU,
                product.Price,
                product.Quantity,
                product.CategoryId
            ));
        }

        /// <summary>
        /// POST /api/products
        /// </summary>
        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            _logger.LogInformation("Creating product with SKU {Sku}", dto.SKU);
            // Pattern matching validation
            if (dto is not
                {
                    Name: { Length: > 0 },
                    SKU: { Length: > 0 },
                    Price: > 0,
                    Quantity: >= 0
                })
            {
                return BadRequest("Invalid product data");
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                SKU = dto.SKU,
                Price = dto.Price,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId
            };

            _repository.Add(product);
            _searchEngine.ClearCache();

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                new ProductDto(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.SKU,
                    product.Price,
                    product.Quantity,
                    product.CategoryId
                )
            );
        }

        /// <summary>
        /// PUT /api/products/{id}
        /// Updates an existing product
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateProductDto dto)
        {
            // Pattern matching validation
            if (dto is not
                {
                    Name: { Length: > 0 },
                    SKU: { Length: > 0 },
                    Price: > 0,
                    Quantity: >= 0
                })
            {
                return BadRequest("Invalid product data");
            }

            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.SKU = dto.SKU;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.CategoryId = dto.CategoryId;

            _repository.Update(product);
            _searchEngine.ClearCache();

            return NoContent();
        }

        /// <summary>
        /// DELETE /api/products/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _repository.Delete(id);
            if (!deleted)
                return NotFound();
            _searchEngine.ClearCache();

            return NoContent();
        }

        /// <summary>
        /// Demonstrates manual model binding (NO DTOs, NO attributes)
        /// </summary>
        [HttpGet("manual-search")]
        public IActionResult ManualSearch()
        {
            // 🔹 Manually read values from the query string
            var name = Request.Query["name"].ToString();
            var minPriceRaw = Request.Query["minPrice"].ToString();

            // 🔹 Manual parsing + validation
            if (!decimal.TryParse(minPriceRaw, out var minPrice))
            {
                minPrice = 0;
            }

            var products = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p =>
                    p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            products = products.Where(p => p.Price >= minPrice);

            var result = products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.SKU,
                p.Price,
                p.Quantity,
                p.CategoryId
            ));

            return Ok(result);
        }

        /// <summary>
        /// Demonstrates custom JSON serialization
        /// </summary>
        [HttpGet("custom-json")]
        public IActionResult GetWithCustomJson()
        {
            var products = _repository.GetAll()
                .Select(p => new
                {
                    product_id = p.Id,          // custom naming
                    product_name = p.Name,
                    price = Math.Round(p.Price, 2),
                    in_stock = p.Quantity > 0
                });

            // 🔹 Custom JSON options (LOCAL to this endpoint)
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,              // pretty print
                PropertyNamingPolicy = null        // keep names EXACT
            };

            var json = JsonSerializer.Serialize(products, jsonOptions);

            // 🔹 Return raw JSON manually
            return Content(json, "application/json");
        }

    }
}

