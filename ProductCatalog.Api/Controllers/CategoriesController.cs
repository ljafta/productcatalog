using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.DTOs;
using ProductCatalog.Api.Extensions;
using ProductCatalog.Api.Repositories.InMemory;

namespace ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly InMemoryCategoryRepository _repository;

        public CategoriesController(InMemoryCategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /api/categories
        /// Returns flat list of categories
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _repository.GetAll()
                .Select(c => new CategoryDto(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ParentCategoryId
                ));

            return Ok(categories);
        }

        /// <summary>
        /// GET /api/categories/tree
        /// Returns hierarchical category tree
        /// </summary>
        [HttpGet("tree")]
        public IActionResult GetTree()
        {
            var categories = _repository.GetAll();
            var tree = CategoryTreeBuilder.BuildTree(categories);
            return Ok(tree);
        }

        /// <summary>
        /// POST /api/categories
        /// Creates a category (root or child)
        /// </summary>
        [HttpPost]
        public IActionResult Create(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                ParentCategoryId = dto.ParentCategoryId
            };

            _repository.Add(category);

            return Ok();
        }

    }
}
