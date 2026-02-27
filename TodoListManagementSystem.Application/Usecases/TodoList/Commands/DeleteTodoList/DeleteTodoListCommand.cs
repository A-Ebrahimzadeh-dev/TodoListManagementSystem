using MediatR;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commands.DeleteTodoList
{
    public record DeleteTodoListCommand(Guid UserId, Guid TodoListId) : IRequest<Unit>;
}
