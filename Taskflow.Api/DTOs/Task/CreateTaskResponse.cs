using Taskflow.Api.Enums;

namespace Taskflow.Api.DTOs.Task
{
    public class CreateTaskResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public long? CategoryId { get; set; }
    }
}