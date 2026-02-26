using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Domain.Entities;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.Repositories
{
    public class TodoListRepository(
        IJsonFileStore store,
        IPathProvider paths) : ITodoListRepository
    {
        private readonly IJsonFileStore _store = store;
        private readonly IPathProvider _paths = paths;

        public async Task<TodoList> SaveAsync(TodoList todoList, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(todoList.Creator, todoList.Id);
            await _store.WriteAsync(path, todoList, ct);
            return todoList;
        }

        public async Task<bool> DeleteByIdAsync(Guid userId, Guid todoListId, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(userId, todoListId);

            if (!await _store.ExistsAsync(path, ct))
                return false;

            await _store.DeleteAsync(path, ct);
            return true;
        }

        public async Task<TodoList?> GetByIdAsync(Guid userId, Guid todoListId, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(userId, todoListId);
            return await _store.ReadAsync<TodoList>(path, ct);
        }

        public async Task<IReadOnlyList<TodoList>> GetByUserIdAsync(Guid userId, CancellationToken ct)
        {
            var dir = _paths.GetUserTodoListsDirectory(userId);
            var allTodoLists = await _store.ReadAllFromDirectoryAsync<TodoList>(dir, ct);
            return allTodoLists.ToList();
        }

        public async Task<bool> ExistsAsync(Guid userId, string title, string? description, CancellationToken ct)
        {
            var dir = _paths.GetUserTodoListsDirectory(userId);
            var todoLists = await _store.ReadAllFromDirectoryAsync<TodoList>(dir, ct);
            return todoLists.Any(t => t.Title == title && t.Description == description);
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid todoListId, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(userId, todoListId);
            return await _store.ExistsAsync(path, ct);
        }
    }
}
