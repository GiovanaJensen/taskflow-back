using Taskflow.Api.DTOs.Category;

namespace Taskflow.Api.Services
{
    public interface ICategoryService
    {
        Task<CreateCategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request);
    }
}
