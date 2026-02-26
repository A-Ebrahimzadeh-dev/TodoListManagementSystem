using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commands.UpdateTodoList
{
    public record UpdateTodoListCommand(Guid UserId, Guid TodoListId, string Title, string? Description) : IRequest<TodoListDto>;
}
