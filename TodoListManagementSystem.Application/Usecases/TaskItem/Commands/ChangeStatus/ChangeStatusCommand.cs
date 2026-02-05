using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.ChangeStatus
{
    public record ChangeStatusCommand(
        Guid UserId,
        Guid TodoListId,
        Guid TaskItemId,
        Status Status) : IRequest<TaskItemDto>;
}
