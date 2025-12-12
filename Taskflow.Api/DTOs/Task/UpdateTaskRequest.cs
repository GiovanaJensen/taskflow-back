using Taskflow.Api.Enums;

namespace Taskflow.Api.DTOs.Task
{
    public record UpdateTaskRequest(long Id, string Title, string? Description, TaskPriority TaskPriority, DateTime? DueDate,long? CategoryId, bool IsCompleted);
}