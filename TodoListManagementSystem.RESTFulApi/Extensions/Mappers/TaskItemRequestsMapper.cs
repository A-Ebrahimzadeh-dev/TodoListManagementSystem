using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.ChangePriorityLevel;
using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.ChangeStatus;
using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.CreateTaskItem;
using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.DeleteTaskItem;
using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags;
using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTaskItem;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers
{
    public static class TaskItemRequestsMapper
    {
        extension(CreateTaskItemRequest request)
        {
            public CreateTaskItemCommand MapToCreateTaskItemCommand(Guid UserId)
            {
                return new CreateTaskItemCommand(
                    UserId,
                    request.TodoListId,
                    request.Title,
                    request.Description,
                    request.PriorityLevel,
                    request.DueDate,
                    request.Tags);
            }
        }

        extension(DeleteTaskItemRequest request)
        {
            public DeleteTaskItemCommand MapToDeleteTaskItemCommand(Guid userId, Guid taskItemId)
            {
                return new(userId, request.TodoListId, taskItemId);
            }
        }

        extension(UpdateTaskItemPriorityLevelRequest request)
        {
            public ChangePriorityLevelCommand MapToChangePriorityLevelCommand(Guid userId, Guid taskItemId)
            {
                return new(userId, request.TodoListId, taskItemId, request.PriorityLevel);
            }
        }

        extension(UpdateTaskItemRequest request)
        {
            public UpdateTaskItemCommand MapToUpdateTaskItemCommand(Guid userId, Guid taskItemId)
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

        extension(UpdateTaskItemStatusRequest request)
        {
            public ChangeStatusCommand MapToChangeTaskItemStatusCommand(Guid userId, Guid taskItemId)
            {
                return new(userId, request.TodoListId, taskItemId, request.Status);
            }
        }

        extension(UpdateTaskItemTagsRequest request)
        {
            public UpdateTagsCommand MapToTaskItemTagsCommand(Guid userId, Guid taskItemId)
            {
                return new(userId, request.TodoListId, taskItemId, request.NewTags);
            }
        }
    }
}
