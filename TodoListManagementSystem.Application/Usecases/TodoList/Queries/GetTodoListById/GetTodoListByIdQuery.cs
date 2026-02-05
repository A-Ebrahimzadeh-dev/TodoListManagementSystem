using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetTodoListById
{
    public record GetTodoListByIdQuery(Guid TodoListId, Guid UserId) : IRequest<TodoListDto>;
}
