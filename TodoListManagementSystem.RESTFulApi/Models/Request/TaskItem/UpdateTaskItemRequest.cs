using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem
{
    public record UpdateTaskItemRequest(
        Guid TodoListId,
        string Title,
        string? Description,
        PriorityLevel PriorityLevel,
        Status Status,
        DateTime? DueDate,
        List<string>? Tags) : IRequest<TaskItemDto>;
}
