using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commads.CreateTodoList
{
    public record CreateTodoListCommand(Guid Creator, string Title, string? Description) : IRequest<TodoListDto>;
}
