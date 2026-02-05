using MediatR;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commads.DeleteTodoList
{
    public record DeleteTodoListCommand(Guid UserId, Guid TodoListId) : IRequest<Unit>;
}
