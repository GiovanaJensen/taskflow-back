namespace Taskflow.Api.DTOs.Category
{
    public record UpdateCategoryRequest(long Id, string Name, string? Description);
}