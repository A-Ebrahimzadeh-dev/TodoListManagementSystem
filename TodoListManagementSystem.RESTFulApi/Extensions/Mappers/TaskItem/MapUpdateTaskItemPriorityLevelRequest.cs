using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.ChangePriorityLevel;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TaskItem
{
    public static class MapUpdateTaskItemPriorityLevelRequest
    {
        public static ChangePriorityLevelCommand MapToChangePriorityLevelCommand(this UpdateTaskItemPriorityLevelRequest request, Guid userId, Guid taskItemId)
        {
            return new(userId, request.TodoListId, taskItemId, request.PriorityLevel);
        }
    }
}
