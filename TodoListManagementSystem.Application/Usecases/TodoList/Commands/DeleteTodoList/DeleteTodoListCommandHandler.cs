using MediatR;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commands.DeleteTodoList
{
    public class DeleteTodoListCommandHandler
        (ITodoListRepository todoListRepository) : IRequestHandler<DeleteTodoListCommand, Unit>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _todoListRepository
                .DeleteByIdAsync(request.UserId, request.TodoListId, cancellationToken);

            if (!deleted)
                throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found for user [{request.UserId}].");

            return Unit.Value;
        }
    }
}