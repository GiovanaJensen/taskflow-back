using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskflow.Api.DTOs.Auth;
using Taskflow.Api.DTOs.Category;
using Taskflow.Api.DTOs.Task;
using Taskflow.Api.Enums;
using Taskflow.Api.Services;
using Taskflow.Api.Services.Auth;

namespace Taskflow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var created = await _taskService.CreateTaskAsync(request);
            return Ok(created);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTask(UpdateTaskRequest request)
        {
            var updated = await _taskService.UpdateTaskAsync(request);
            return Ok(updated);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            return Ok(deleted);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetTasksByCategoryId(long categoryId)
        {
            var tasks = await _taskService.GetTasksByCategoryIdAsync(categoryId);
            return Ok(tasks);
        }

        [HttpGet("priority/{priorityId}")]
        public async Task<IActionResult> GetTasksByPriority([FromRoute] TaskPriority priorityId)
        {
            var tasks = await _taskService.GetTasksByPriorityAsync(priorityId);
            return Ok(tasks);
        }
    }

}