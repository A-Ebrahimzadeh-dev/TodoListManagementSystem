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
            var existingTodoList = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken)
                ?? throw new SourceNotFoundException($"Tood list with id [{request.TodoListId}] not found");
            if (existingTodoList.Creator != request.UserId)
                throw new AccessDeniedException($"User [{request.UserId}] not have permission to read this todo list.");

            return existingTodoList.MapToTodoListDto();
        }
    }
}
