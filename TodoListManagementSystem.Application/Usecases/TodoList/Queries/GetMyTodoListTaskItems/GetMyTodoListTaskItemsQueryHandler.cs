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
            var todoListExists = await _todoListRepository
                .ExistsAsync(request.UserId, request.TodoListId, cancellationToken);

            if (!todoListExists)
                throw new SourceNotFoundException($"Todo list with id {request.TodoListId} not found for user {request.UserId}.");

            var taskItems = await _taskItemRepository
                .GetByTodoListIdAsync(request.UserId, request.TodoListId, cancellationToken);

            return [.. taskItems.Select(t => t.MapToTaskItemDto())];
        }
    }
}
