using MediatR;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commads.DeleteTodoList
{
    public class DeleteTodoListCommandHandler(ITodoListRepository todoListRepository) : IRequestHandler<DeleteTodoListCommand, Unit>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = await ValidateAccessAsync(request, cancellationToken);

            await _todoListRepository.DeleteByIdAsync(todoList.Id, cancellationToken);

            return Unit.Value;
        }

        private async Task<Domain.Entities.TodoList> ValidateAccessAsync(DeleteTodoListCommand request, CancellationToken ct)
        {
            var existingTodoList = await _todoListRepository.GetByIdAsync(request.TodoListId, ct)
                ?? throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found.");
            if (existingTodoList.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to delete this todo list.");

            return existingTodoList;
        }
    }
}