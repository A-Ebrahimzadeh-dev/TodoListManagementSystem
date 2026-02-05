using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.DeleteTaskItem;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TaskItem
{
    public static class MapDeleteTaskItemRequest
    {
        public static DeleteTaskItemCommand MapToDeleteTaskItemCommand(this DeleteTaskItemRequest request, Guid userId, Guid taskItemId)
        {
            return new(userId, request.TodoListId, taskItemId);
        }
    }
}
