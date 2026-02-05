using MediatR;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetMyTodoLists
{
    public class GetMyTodoListsQueryHandler(ITodoListRepository todoListRepository) : IRequestHandler<GetMyTodoListTaskItemsQuery, IReadOnlyCollection<TodoListDto>>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;

        public async Task<IReadOnlyCollection<TodoListDto>> Handle(GetMyTodoListTaskItemsQuery request, CancellationToken cancellationToken)
        {
            var allTodoLists = await _todoListRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            return [.. allTodoLists.Select(t => t.MapToTodoListDto())];
        }
    }
}
