using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem
{
    public record UpdateTaskItemStatusRequest(Guid TodoListId, Status Status);
}
