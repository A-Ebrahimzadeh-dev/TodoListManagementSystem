using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commads.UpdateTodoList
{
    public class UpdateTodoListCommandHandler(ITodoListRepository todoListRepository) : IRequestHandler<UpdateTodoListCommand, TodoListDto>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TodoListDto> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = await ValidateAccessAsync(request, cancellationToken);

            todoList.UpdateContent(request.Title, request.Description);
            todoList.UpdateLastModifiedDate();

            await _todoListRepository.SaveAsync(todoList, cancellationToken);

            return todoList.MapToTodoListDto();
        }

        private async Task<Domain.Entities.TodoList> ValidateAccessAsync(UpdateTodoListCommand request, CancellationToken ct)
        {
            var existingTodoList = await _todoListRepository.GetByIdAsync(request.TodoListId, ct)
                ?? throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found.");
            if (existingTodoList.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to delete this todo list.");

            return existingTodoList;
        }
    }
}
