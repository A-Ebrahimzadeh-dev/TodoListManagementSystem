using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Domain.Entities;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.Repositories
{
    public class TaskItemRepository(
        IJsonFileStore store,
        IPathProvider paths) : ITaskItemRepository
    {
        private readonly IJsonFileStore _store = store;
        private readonly IPathProvider _paths = paths;

        public async Task<TaskItem> SaveAsync(Guid userId, TaskItem taskItem, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(userId, taskItem.TodoListId, taskItem.Id);
            await _store.WriteAsync(path, taskItem, ct);
            return taskItem;
        }

        public async Task<bool> DeleteByIdAsync(Guid userId, Guid todoListId, Guid taskItemId, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(userId, todoListId, taskItemId);

            if (!await _store.ExistsAsync(path, ct))
                return false;

            await _store.DeleteAsync(path, ct);
            return true;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid userId, Guid todoListId, Guid taskItemId, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(userId, todoListId, taskItemId);
            return await _store.ReadAsync<TaskItem>(path, ct);
        }

        public async Task<IReadOnlyList<TaskItem>> GetByTodoListIdAsync(Guid userId, Guid todoListId, CancellationToken ct)
        {
            var dir = _paths.GetTaskItemsDirectory(userId, todoListId);
            return await _store.ReadAllFromDirectoryAsync<TaskItem>(dir, ct);
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid todoListId, Guid taskItemId, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(userId, todoListId, taskItemId);
            return await _store.ExistsAsync(path, ct);
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid todoListId, string title, DateTime? dueDate, CancellationToken ct)
        {
            var dir = _paths.GetTaskItemsDirectory(userId, todoListId);
            var taskItems = await _store.ReadAllFromDirectoryAsync<TaskItem>(dir, ct);
            return taskItems.Any(t => t.Title == title && t.DueDate == dueDate);
        }
    }
}
