using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Taskflow.Api.Data;
using Taskflow.Api.DTOs.Category;
using Taskflow.Api.Exceptions;
using Taskflow.Api.Models;

namespace Taskflow.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly TaskflowDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(TaskflowDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateCategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request)
        {
            if (await _db.categories.AnyAsync(c => c.Name == request.Name))
                throw new ConflictException("Categoria já existe.");  

            var userId = long.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                UserId = userId
            };

            _db.categories.Add(category);
            await _db.SaveChangesAsync();

            return new CreateCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}