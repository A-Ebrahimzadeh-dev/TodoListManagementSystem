using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetMyTodoListTaskItems
{
    public class GetMyTodoListTaskItemsQueryHandler(ITodoListRepository todoListRepository, ITaskItemRepository taskItemRepository) : IRequestHandler<GetMyTodoListsTaskItemsQuery, IReadOnlyCollection<TaskItemDto>>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;

        public async Task<IReadOnlyCollection<TaskItemDto>> Handle(GetMyTodoListsTaskItemsQuery request, CancellationToken cancellationToken)
        {
            var existingTodolist = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken)
                ?? throw new SourceNotFoundException($"Todo list [{request.TodoListId}] not found");

            if (existingTodolist.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to read this todo list [{request.TodoListId}].");

            var taskItems = await _taskItemRepository.GetByTodoListIdAsync(request.TodoListId, cancellationToken);

            return [.. taskItems.Select(t => t.MapToTaskItemDto())];
        }
    }
}
