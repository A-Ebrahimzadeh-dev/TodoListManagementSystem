using TodoListManagementSystem.Application.Usecases.TodoList.Commands.CreateTodoList;
using TodoListManagementSystem.Application.Usecases.TodoList.Commands.UpdateTodoList;
using TodoListManagementSystem.RESTFulApi.Models.Request.TodoList;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers
{
    public static class TodoListRequestsMapper
    {
        extension(CreateTodoListRequest request)
        {
            public CreateTodoListCommand MapToCreateTodoListCommand(Guid userId)
            {
                return new(userId, request.Title, request.Description);
            }
        }

        extension(UpdateTodoListRequest request)
        {
            public UpdateTodoListCommand MapToUpdateTodoListCommand(Guid userId, Guid todoListId)
            {
                return new(userId, todoListId, request.Title, request.Description);
            }
        }
    }
}
