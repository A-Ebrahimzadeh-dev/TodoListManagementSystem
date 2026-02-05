namespace TodoListManagementSystem.Application.DTOs.TodoList
{
    public class TodoListDto
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public required DateTime CreatedDate { get; set; }
        public required DateTime LastModifiedDate { get; set; }
    }
}