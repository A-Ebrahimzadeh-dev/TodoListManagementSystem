using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commands.CreateTodoList
{
    public class CreateTodoListCommandHandler(
        ITodoListRepository todoListRepository) : IRequestHandler<CreateTodoListCommand, TodoListDto>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TodoListDto> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoListExists = await _todoListRepository.ExistsAsync(request.Creator, request.Title, request.Description, cancellationToken);
            if (todoListExists)
                throw new SourceAlreadyExistsException($"Todo list with title '{request.Title}' and description '{request.Description}' already exists.");

            var todoList = Domain.Entities.TodoList.CreateNewTodoList(request.Creator, request.Title, request.Description);

            await _todoListRepository.SaveAsync(todoList, cancellationToken);

            return todoList.MapToTodoListDto();
        }
    }
}
