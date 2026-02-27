using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetTodoListById
{
    public class GetTodoListByIdQueryHandler(ITodoListRepository todoListRepository) : IRequestHandler<GetTodoListByIdQuery, TodoListDto>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<TodoListDto> Handle(GetTodoListByIdQuery request, CancellationToken cancellationToken)
        {
            var existingTodoList = await _todoListRepository.GetByIdAsync(request.UserId, request.TodoListId, cancellationToken)
                ?? throw new SourceNotFoundException($"Todo list with id [{request.TodoListId}] not found");

            return existingTodoList.MapToTodoListDto();
        }
    }
}
