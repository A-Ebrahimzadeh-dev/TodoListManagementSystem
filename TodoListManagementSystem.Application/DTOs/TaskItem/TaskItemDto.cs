using System.Collections;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.DTOs.TaskItem
{
    public class TaskItemDto
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public required PriorityLevel PriorityLevel { get; set; }
        public required Status Status { get; set; }
        public required DateTime? DueDate { get; set; }
        public IEnumerable<string> Tags { get; set; } = [];
        public required DateTime CreatedDate { get; set; }
        public required DateTime LastModifiedDate { get; set; }
    }
}
