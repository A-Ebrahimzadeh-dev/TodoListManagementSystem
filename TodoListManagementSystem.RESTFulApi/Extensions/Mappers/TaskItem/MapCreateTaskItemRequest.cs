using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.CreateTaskItem;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TaskItem
{
    public static class MapCreateTaskItemRequest
    {
        public static CreateTaskItemCommand MapToCreateTaskItemCommand(this CreateTaskItemRequest request, Guid UserId)
        {
            return new CreateTaskItemCommand(UserId, request.TodoListId, request.Title, request.Description, request.PriorityLevel, request.DueDate, request.Tags);
        }
    }
}
