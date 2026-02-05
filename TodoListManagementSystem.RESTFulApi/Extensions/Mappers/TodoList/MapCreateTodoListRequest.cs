using TodoListManagementSystem.Application.Usecases.TodoList.Commads.CreateTodoList;
using TodoListManagementSystem.RESTFulApi.Models.Request.TodoList;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TodoList
{
    public static class MapCreateTodoListRequest
    {
        public static CreateTodoListCommand MapToCreateTodoListCommand(this CreateTodoListRequest request, Guid userId)
        {
            return new(userId, request.Title, request.Description);
        }
    }
}
