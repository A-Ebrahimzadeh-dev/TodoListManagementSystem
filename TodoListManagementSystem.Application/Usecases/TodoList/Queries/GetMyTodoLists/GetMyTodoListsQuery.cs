using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetMyTodoLists
{
    public record GetMyTodoListTaskItemsQuery(Guid UserId) : IRequest<IReadOnlyCollection<TodoListDto>>;
}
