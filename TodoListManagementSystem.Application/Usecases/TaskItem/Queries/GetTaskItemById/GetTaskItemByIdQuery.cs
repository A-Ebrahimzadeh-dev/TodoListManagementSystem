using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Queries.GetTaskItemById
{
    public record GetTaskItemByIdQuery(Guid UserId, Guid TodoListId, Guid TaskItemId) : IRequest<TaskItemDto>;
}
