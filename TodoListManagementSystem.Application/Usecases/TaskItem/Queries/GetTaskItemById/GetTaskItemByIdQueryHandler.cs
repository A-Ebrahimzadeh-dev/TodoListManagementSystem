using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Queries.GetTaskItemById
{
    public class GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository) : IRequestHandler<GetTaskItemByIdQuery, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;

        public async Task<TaskItemDto> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var existingTaskItem = await _taskItemRepository.GetByIdAsync(request.UserId, request.TodoListId, request.TaskItemId, cancellationToken)
                ?? throw new SourceNotFoundException($"Task item with ID [{request.TaskItemId}] not found for user [{request.UserId}] in TodoList [{request.TodoListId}].");

            return existingTaskItem.MapToTaskItemDto();
        }
    }
}
