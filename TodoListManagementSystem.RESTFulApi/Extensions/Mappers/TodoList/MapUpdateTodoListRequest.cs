using TodoListManagementSystem.Application.Usecases.TodoList.Commads.UpdateTodoList;
using TodoListManagementSystem.RESTFulApi.Models.Request.TodoList;

namespace TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TodoList
{
    public static class MapUpdateTodoListRequest
    {
        public static UpdateTodoListCommand MapToUpdateTodoListCommand(this UpdateTodoListRequest request, Guid userId, Guid todoListId)
        {
            return new(userId, todoListId, request.Title, request.Description);
        }
    }
}
