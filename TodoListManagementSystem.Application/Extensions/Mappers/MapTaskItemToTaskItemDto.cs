using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Extensions.Mappers
{
    public static class MapTaskItemToTaskItemDto
    {
        public static TaskItemDto MapToTaskItemDto(this TaskItem item)
        {
            return new TaskItemDto()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description ?? string.Empty,
                PriorityLevel = item.Priority,
                Status = item.Status,
                DueDate = item.DueDate,
                Tags = item.Tags ?? [],
                CreatedDate = item.CreatedDate,
                LastModifiedDate = item.LastModifiedDate,
            };
        }
    }
}
