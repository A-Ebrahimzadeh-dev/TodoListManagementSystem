using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Extensions.Mappers
{
    public static class MapTodoListToTodoListDto
    {
        public static TodoListDto MapToTodoListDto(this TodoList item)
        {
            return new TodoListDto()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description ?? string.Empty,
                CreatedDate = item.CreatedDate,
                LastModifiedDate = item.LastModifiedDate,
            };
        }
    }
}
