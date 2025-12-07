using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskflow.Api.DTOs.Auth;
using Taskflow.Api.DTOs.Category;
using Taskflow.Api.Services;
using Taskflow.Api.Services.Auth;

namespace Taskflow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var created = await _categoryService.CreateCategoryAsync(request);
            return Ok(created);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
        {
            var updated = await _categoryService.UpdateCategoryAsync(request);
            return Ok(updated);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            return Ok(deleted);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

    }

}