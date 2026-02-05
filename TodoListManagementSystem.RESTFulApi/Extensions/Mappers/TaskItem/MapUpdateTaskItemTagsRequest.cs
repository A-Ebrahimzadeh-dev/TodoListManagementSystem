using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TaskItem
{
    public static class MapUpdateTaskItemTagsRequest
    {
        public static UpdateTagsCommand MapToTaskItemTagsCommand(this UpdateTaskItemTagsRequest request, Guid userId, Guid taskItemId)
        {
            return new(userId, request.TodoListId, taskItemId, request.NewTags);
        }
    }
}
