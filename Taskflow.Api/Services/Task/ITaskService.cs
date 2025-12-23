using Taskflow.Api.DTOs.Task;
using Taskflow.Api.Enums;

namespace Taskflow.Api.Services
{
    public interface ITaskService
    {
        Task<CreateTaskResponse?> CreateTaskAsync(CreateTaskRequest request);
        Task<CreateTaskResponse?> UpdateTaskAsync(UpdateTaskRequest request);
        Task<CreateTaskResponse?> FinishTaskAsync(FinishTaskRequest request);
        Task<List<GetTasksResponse>> GetTasksAsync(bool isCompleted);
        Task<List<GetTasksResponse>> GetTasksByCategoryIdAsync(long categoryId, bool isCompleted);
        Task<List<GetTasksResponse>> GetTasksByPriorityAsync(TaskPriority priority);
        Task<string?> DeleteTaskAsync(long id);
    }
}
