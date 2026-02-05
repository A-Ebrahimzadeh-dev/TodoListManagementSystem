using MediatR;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.DeleteTaskItem
{
    public record DeleteTaskItemCommand(Guid UserId, Guid TodoListId, Guid TaskItemId) : IRequest<Unit>;
}
