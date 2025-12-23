using Taskflow.Api.Enums;

namespace Taskflow.Api.DTOs.Task
{
    public record FinishTaskRequest(long Id, bool IsCompleted);
}