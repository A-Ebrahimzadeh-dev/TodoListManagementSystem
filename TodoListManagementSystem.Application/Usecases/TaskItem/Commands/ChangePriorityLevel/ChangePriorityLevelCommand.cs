using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.ChangePriorityLevel
{
    public record ChangePriorityLevelCommand(Guid UserId, Guid TodoListId, Guid TaskItemId, PriorityLevel PriorityLevel) : IRequest<TaskItemDto>;
}
