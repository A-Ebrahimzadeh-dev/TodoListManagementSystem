using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetMyTodoListTaskItems
{
    public record GetMyTodoListsTaskItemsQuery(Guid UserId, Guid TodoListId) : IRequest<IReadOnlyCollection<TaskItemDto>>;

}
