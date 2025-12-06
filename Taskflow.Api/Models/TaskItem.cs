
using Taskflow.Api.Enums;

namespace Taskflow.Api.Models
{
    public class TaskItem
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public long? CategoryId { get; set; }
        public Category? Category { get; set; }

        public long UserId { get; set; }
        public User User { get; set; } = default!;
    }

}