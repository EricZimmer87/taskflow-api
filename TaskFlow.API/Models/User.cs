namespace TaskFlow.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
