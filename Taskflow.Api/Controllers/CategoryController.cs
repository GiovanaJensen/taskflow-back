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

    }

}