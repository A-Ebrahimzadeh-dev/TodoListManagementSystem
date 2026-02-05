using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem
{
    public record UpdateTaskItemPriorityLevelRequest(Guid TodoListId, PriorityLevel PriorityLevel);
}
