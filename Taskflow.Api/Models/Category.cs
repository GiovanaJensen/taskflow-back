
namespace Taskflow.Api.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public long UserId { get; set; }
        public User User { get; set; } = default!;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
