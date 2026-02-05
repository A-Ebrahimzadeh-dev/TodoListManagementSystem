using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.CreateTaskItem
{
    public record CreateTaskItemCommand(
        Guid UserId,
        Guid TodoListId,
        string Title,
        string? Description,
        PriorityLevel PriorityLevel,
        DateTime? DueDate,
        List<string>? Tags) : IRequest<TaskItemDto>;
}
