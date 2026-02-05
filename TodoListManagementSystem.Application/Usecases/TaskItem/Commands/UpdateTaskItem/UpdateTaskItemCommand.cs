using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTaskItem
{
    public record UpdateTaskItemCommand(
        Guid UserId,
        Guid TodoListId,
        Guid TaskItemId,
        string Title,
        string? Description,
        PriorityLevel PriorityLevel,
        Status Status,
        DateTime? DueDate,
        List<string>? Tags) : IRequest<TaskItemDto>;
}
