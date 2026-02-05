using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commads.CreateTodoList
{
    public class CreateTodoListCommandHandler(
        IUserRepository userRepository,
        ITodoListRepository todoListRepository) : IRequestHandler<CreateTodoListCommand, TodoListDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TodoListDto> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            await ValidateAccessAsync(request, cancellationToken);

            var existingTodoList = await _todoListRepository.ExistsAsync(request.Title, request.Description, cancellationToken);
            if (existingTodoList)
                throw new SourceAlreadyExistsException($"Todo list with title '{request.Title}' and description '{request.Description}' already exists.");

            var todoList = Domain.Entities.TodoList.CreateNewTodoList(request.Creator, request.Title, request.Description);

            var newTodoList = await _todoListRepository.SaveAsync(todoList, cancellationToken);

            return newTodoList.MapToTodoListDto();
        }

        private async Task<bool> ValidateAccessAsync(CreateTodoListCommand command, CancellationToken ct)
        {
            var _ = _userRepository.GetByIdAsync(command.Creator, ct)
                ?? throw new UserUnauthorizedException($"User with ID [{command.Creator}] not found.");
            return true;
        }
    }
}
