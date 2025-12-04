namespace Taskflow.Api.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // em demo, senha em texto (NÃO usar em prod)
        public string Role { get; set; } = "User";
    }
}