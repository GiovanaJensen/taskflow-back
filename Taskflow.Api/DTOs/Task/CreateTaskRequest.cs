using Taskflow.Api.Enums;

namespace Taskflow.Api.DTOs.Task
{
    public record CreateTaskRequest(string Title, string? Description, TaskPriority TaskPriority, DateTime? DueDate,long? CategoryId);
}