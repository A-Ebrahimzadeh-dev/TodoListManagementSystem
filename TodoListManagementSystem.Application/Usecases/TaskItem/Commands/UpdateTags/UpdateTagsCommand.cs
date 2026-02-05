using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags
{
    public record UpdateTagsCommand(
        Guid UserId,
        Guid TodoListId,
        Guid TaskItemId,
        List<string>? NewTags) : IRequest<TaskItemDto>;
}
