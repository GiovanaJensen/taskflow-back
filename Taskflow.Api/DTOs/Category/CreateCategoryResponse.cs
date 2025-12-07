namespace Taskflow.Api.DTOs.Category
{
    public class CreateCategoryResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}