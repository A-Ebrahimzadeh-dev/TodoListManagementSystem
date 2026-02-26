using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Extensions.Mappers
{
    public static class TodoListMapper
    {
        extension(TodoList item)
        {
            public TodoListDto MapToTodoListDto()
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
}
