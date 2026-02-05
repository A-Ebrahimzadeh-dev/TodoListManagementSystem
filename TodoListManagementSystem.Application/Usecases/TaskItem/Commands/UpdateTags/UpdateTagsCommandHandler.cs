using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags
{
    public class UpdateTagsCommandHandler(
        ITaskItemRepository taskItemRepository,
        ITodoListRepository todoListRepository) : IRequestHandler<UpdateTagsCommand, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TaskItemDto> Handle(UpdateTagsCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await ValidateAccessAsync(request, cancellationToken);

            taskItem.UpdateTagsList(request.NewTags);
            taskItem.UpdateLastModifiedDate();

            await _taskItemRepository.SaveAsync(taskItem, cancellationToken);

            return taskItem.MapToTaskItemDto();
        }

        private async Task<Domain.Entities.TaskItem> ValidateAccessAsync(UpdateTagsCommand request, CancellationToken ct)
        {
            var existingTodoList = await _todoListRepository.GetByIdAsync(request.TodoListId, ct)
                ?? throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found.");
            if (existingTodoList.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to update this task item.");

            var existingTaskItem = await _taskItemRepository.GetByIdAsync(request.TaskItemId, ct)
                ?? throw new SourceNotFoundException($"Task item with ID [{request.TaskItemId}] not found");
            if (existingTaskItem.TodoListId != request.TodoListId)
                throw new AccessDeniedException($"Task item with ID [{request.TaskItemId}] does not belong to the specified TodoList.");

            return existingTaskItem;
        }
    }
}
