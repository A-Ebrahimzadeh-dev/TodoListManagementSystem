using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Domain.Entities;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem.Repositories
{
    public class TodoListRepository(
        IJsonFileStore store,
        IPathProvider paths) : ITodoListRepository
    {
        private readonly IJsonFileStore _store = store;
        private readonly IPathProvider _paths = paths;

        public async Task DeleteByIdAsync(Guid id, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(id);
            await _store.DeleteAsync(path, ct);
        }

        public async Task<bool> ExistsAsync(string title, string? description, CancellationToken ct)
        {
            var path = _paths.TodoListsDirectory;
            var taskItems = await _store.ReadAllFromDirectoryAsync<TodoList>(path, ct);

            return taskItems.Any(t =>
                t.Title == title &&
                t.Description == description);
        }

        public async Task<IReadOnlyList<TodoList>> GetByUserIdAsync(Guid userId, CancellationToken ct)
        {
            var path = _paths.TodoListsDirectory;

            var allTodoLists =
                await _store.ReadAllFromDirectoryAsync<TodoList>(path, ct);

            return [.. allTodoLists.Where(t => t.Creator == userId)];
        }

        public async Task<TodoList?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(id);
            return await _store.ReadAsync<TodoList>(path, ct);
        }

        public async Task<TodoList> SaveAsync(TodoList todoList, CancellationToken ct)
        {
            var path = _paths.GetTodoListFilePath(todoList.Id);
            await _store.WriteAsync(path, todoList, ct);
            return todoList;
        }
    }
}
