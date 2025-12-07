using Taskflow.Api.DTOs.Category;

namespace Taskflow.Api.Services
{
    public interface ICategoryService
    {
        Task<CreateCategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request);
        Task<CreateCategoryResponse?> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<List<GetCategoriesResponse>> GetCategoriesAsync();
        Task<string?> DeleteCategoryAsync(long id);
    }
}
