namespace TaskFlow.API.DTOs
{
    public class UpdateTaskItemRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
