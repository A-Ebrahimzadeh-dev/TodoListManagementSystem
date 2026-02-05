using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.ChangeStatus;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TaskItem
{
    public static class MapUpdateTaskItemStatusRequest
    {
        public static ChangeStatusCommand MapToChangeTaskItemStatusCommand(this UpdateTaskItemStatusRequest request, Guid userId, Guid taskItemId)
        {
            return new(userId, request.TodoListId, taskItemId, request.Status);
        }
    }
}
