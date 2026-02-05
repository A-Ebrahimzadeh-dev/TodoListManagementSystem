using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem
{
    public record CreateTaskItemRequest(
        Guid TodoListId,
        string Title,
        string? Description,
        PriorityLevel PriorityLevel,
        DateTime? DueDate,
        List<string>? Tags);
}
