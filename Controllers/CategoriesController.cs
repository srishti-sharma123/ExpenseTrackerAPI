using ExpenseTrackerAPI.Models.Domain;
using ExpenseTrackerAPI.Models.DTOs.Category;
using ExpenseTrackerAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = new Category
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            var createdCategory = await _categoryRepository.CreateAsync(category);

            var categoryDto = new CategoryDto
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                CreatedAt = createdCategory.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = categoryDto.Id },
                categoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoriesDto = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt
            });

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt
            };

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Category
            {
                Id = id,
                Name = request.Name
            };

            var updatedCategory = await _categoryRepository.UpdateAsync(category);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                CreatedAt = updatedCategory.CreatedAt
            };

            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCategory = await _categoryRepository.DeleteAsync(id);

            if (deletedCategory == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = deletedCategory.Id,
                Name = deletedCategory.Name,
                CreatedAt = deletedCategory.CreatedAt
            };

            return Ok(categoryDto);
        }
    }
}
