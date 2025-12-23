using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Taskflow.Api.Data;
using Taskflow.Api.DTOs.Category;
using Taskflow.Api.DTOs.Task;
using Taskflow.Api.Enums;
using Taskflow.Api.Exceptions;
using Taskflow.Api.Models;

namespace Taskflow.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskflowDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskService(TaskflowDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateTaskResponse?> CreateTaskAsync(CreateTaskRequest request)
        {
            var ctx = _httpContextAccessor.HttpContext!;
            var userId = long.Parse(ctx.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (request.CategoryId.HasValue)
            {
                bool categoryExists = await _db.categories.AnyAsync(c =>
                    c.Id == request.CategoryId && c.UserId == userId);

                if (!categoryExists)
                    throw new BadRequestException("Categoria não encontrada ou não pertence ao usuário.");
            }

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.TaskPriority,
                DueDate = request.DueDate,
                CategoryId = request.CategoryId,
                UserId = userId
            };

            _db.taskItems.Add(task);
            await _db.SaveChangesAsync();

            return new CreateTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CategoryId = task.CategoryId
            };
        }
        public async Task<CreateTaskResponse?> FinishTaskAsync(FinishTaskRequest request)
        {
            var task = await _db.taskItems.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (task is null)
                throw new BadRequestException("A task não existe na base de dados");

            task.IsCompleted = request.IsCompleted;

            _db.taskItems.Update(task);
            await _db.SaveChangesAsync();

            return new CreateTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CategoryId = task.CategoryId
            };
        }
        public async Task<CreateTaskResponse?> UpdateTaskAsync(UpdateTaskRequest request)
        {
            var task = await _db.taskItems.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (task is null)
                throw new BadRequestException("A task não existe na base de dados");

            if (request.CategoryId.HasValue)
            {
                bool categoryExists = await _db.categories.AnyAsync(c =>
                    c.Id == request.CategoryId && c.UserId == task.UserId);

                if (!categoryExists)
                    throw new BadRequestException("Categoria não encontrada ou não pertence ao usuário.");
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.Priority = request.TaskPriority;
            task.DueDate = request.DueDate;
            task.CategoryId = request.CategoryId;
            task.IsCompleted = request.IsCompleted;

            _db.taskItems.Update(task);
            await _db.SaveChangesAsync();

            return new CreateTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CategoryId = task.CategoryId
            };
        }
        public async Task<string?> DeleteTaskAsync(long id)
        {
            var task = await _db.taskItems.FirstOrDefaultAsync(c => c.Id == id);

            if (task is null)
                throw new BadRequestException("A task não existe na base de dados");

            _db.taskItems.Remove(task);
            await _db.SaveChangesAsync();

            return $"A Task {task.Title} foi excluída com sucesso!";
        }

        public async Task<List<GetTasksResponse>> GetTasksAsync(bool isCompleted)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext!.User
                    .FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var categories = await _db.taskItems
                .Where(c => c.UserId == userId)
                .Select(c => new GetTasksResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Priority = c.Priority,
                    DueDate = c.DueDate,
                    CategoryId = c.CategoryId,
                })
                .ToListAsync();

            if (!isCompleted)
            {
                categories.Select(c => c.IsCompleted == false);
            }

            return categories;
        }
        public async Task<List<GetTasksResponse>> GetTasksByCategoryIdAsync(long categoryId, bool isCompleted)
        {
            var tasks = await _db.taskItems.Where(c => c.CategoryId == categoryId).ToListAsync();

            if (!isCompleted)
            {
                tasks.Select(c => c.IsCompleted == false);
            }

            return tasks.Select(c => new GetTasksResponse
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Priority = c.Priority,
                DueDate = c.DueDate,
                CategoryId = c.CategoryId
            }).ToList();
        }

        public async Task<List<GetTasksResponse>> GetTasksByPriorityAsync(TaskPriority status)
        {
            var tasks = await _db.taskItems.Where(c => c.Priority == status).ToListAsync();

            return tasks.Select(c => new GetTasksResponse
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Priority = c.Priority,
                DueDate = c.DueDate,
                CategoryId = c.CategoryId
            }).ToList();
        }
    }
}