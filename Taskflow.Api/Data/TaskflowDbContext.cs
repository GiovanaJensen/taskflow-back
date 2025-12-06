using Microsoft.EntityFrameworkCore;
using Taskflow.Api.Models;

namespace Taskflow.Api.Data
{
    public class TaskflowDbContext : DbContext
    {
        public TaskflowDbContext(DbContextOptions<TaskflowDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> users { get; set; }
    }
}