namespace Taskflow.Api.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<Category> Categories { get; set; } = new();
        public List<TaskItem> Tasks { get; set; } = new();
    }
}