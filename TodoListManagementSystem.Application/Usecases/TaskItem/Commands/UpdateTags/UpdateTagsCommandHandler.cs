using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags
{
    public class UpdateTagsCommandHandler(
        ITaskItemRepository taskItemRepository) : IRequestHandler<UpdateTagsCommand, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;

        public async Task<TaskItemDto> Handle(UpdateTagsCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(request.UserId, request.TodoListId, request.TaskItemId, cancellationToken)
                ?? throw new SourceNotFoundException($"Task item with ID [{request.TaskItemId}] not found for user [{request.UserId}] in TodoList [{request.TodoListId}].");

            taskItem.ReplaceTags(request.NewTags);
            taskItem.UpdateLastModifiedDate();

            await _taskItemRepository.SaveAsync(request.UserId, taskItem, cancellationToken);

            return taskItem.MapToTaskItemDto();
        }
    }
}
