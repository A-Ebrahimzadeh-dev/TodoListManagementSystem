using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Queries.GetTaskItemById
{
    public class GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository, ITodoListRepository todoListRepository) : IRequestHandler<GetTaskItemByIdQuery, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TaskItemDto> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var existingTaskItem = await _taskItemRepository.GetByIdAsync(request.TaskItemId, cancellationToken)
                ?? throw new SourceNotFoundException($"Task item with id [{request.TaskItemId}] not found");

            var existingTodoList = await _todoListRepository.GetByIdAsync(existingTaskItem.TodoListId, cancellationToken)
                ?? throw new InternalServerException("This task is currently unavailable.\r\n");

            if (existingTodoList.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to read this task item [{request.TaskItemId}].");

            return existingTaskItem.MapToTaskItemDto();
        }
    }
}
