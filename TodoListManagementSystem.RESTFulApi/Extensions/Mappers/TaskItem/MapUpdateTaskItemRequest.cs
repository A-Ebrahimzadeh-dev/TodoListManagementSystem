using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTaskItem;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TaskItem
{
    public static class MapUpdateTaskItemRequest
    {
        public static UpdateTaskItemCommand MapToUpdateTaskItemCommand(this UpdateTaskItemRequest request, Guid userId, Guid taskItemId)
        {
            return new UpdateTaskItemCommand(
                userId,
                request.TodoListId,
                taskItemId,
                request.Title,
                request.Description,
                request.PriorityLevel,
                request.Status,
                request.DueDate,
                request.Tags);
        }
    }
}
