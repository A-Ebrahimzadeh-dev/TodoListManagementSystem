using MediatR;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommandHandler(
        ITaskItemRepository taskItemRepository,
        ITodoListRepository todoListRepository) : IRequestHandler<DeleteTaskItemCommand, Unit>
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<Unit> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await ValidateAccessAsync(request, cancellationToken);

            await _taskItemRepository.DeleteByIdAsync(taskItem.Id, cancellationToken);

            return Unit.Value;
        }

        private async Task<Domain.Entities.TaskItem> ValidateAccessAsync(DeleteTaskItemCommand request, CancellationToken ct)
        {
            var existingTodoList = await _todoListRepository.GetByIdAsync(request.TodoListId, ct)
                ?? throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found.");
            if (existingTodoList.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to delete this task item.");

            var existingTaskItem = await _taskItemRepository.GetByIdAsync(request.TaskItemId, ct)
                ?? throw new SourceNotFoundException($"Task item with ID [{request.TaskItemId}] not found");
            if (existingTaskItem.TodoListId != request.TodoListId)
                throw new AccessDeniedException($"Task item with ID [{request.TaskItemId}] does not belong to the specified TodoList.");

            return existingTaskItem;
        }
    }
}
