using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandHandler(ITodoListRepository todoListRepository) : IRequestHandler<UpdateTodoListCommand, TodoListDto>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TodoListDto> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetByIdAsync(request.UserId, request.TodoListId, cancellationToken)
                ?? throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found for user [{request.UserId}]");

            todoList.UpdateContent(request.Title, request.Description);
            todoList.UpdateLastModifiedDate();

            await _todoListRepository.SaveAsync(todoList, cancellationToken);

            return todoList.MapToTodoListDto();
        }
    }
}
