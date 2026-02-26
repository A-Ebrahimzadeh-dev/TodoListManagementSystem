using MediatR;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommandHandler(
        ITaskItemRepository taskItemRepository) : IRequestHandler<DeleteTaskItemCommand, Unit>
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;

        public async Task<Unit> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _taskItemRepository
                .DeleteByIdAsync(request.UserId, request.TodoListId, request.TaskItemId, cancellationToken);

            if (!deleted)
                throw new SourceNotFoundException($"Task item with ID [{request.TaskItemId}] not found for user [{request.UserId}] in TodoList [{request.TodoListId}].");

            return Unit.Value;
        }
    }
}
