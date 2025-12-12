using Taskflow.Api.DTOs.Task;
using Taskflow.Api.Enums;

namespace Taskflow.Api.Services
{
    public interface ITaskService
    {
        Task<CreateTaskResponse?> CreateTaskAsync(CreateTaskRequest request);
        Task<CreateTaskResponse?> UpdateTaskAsync(UpdateTaskRequest request);
        Task<List<GetTasksResponse>> GetTasksAsync();
        Task<List<GetTasksResponse>> GetTasksByCategoryIdAsync(long categoryId);
        Task<List<GetTasksResponse>> GetTasksByPriorityAsync(TaskPriority priority);
        Task<string?> DeleteTaskAsync(long id);
    }
}
