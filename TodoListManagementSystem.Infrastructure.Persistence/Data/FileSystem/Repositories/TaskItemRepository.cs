using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Domain.Entities;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem.Repositories
{
    public class TaskItemRepository(
        IJsonFileStore store,
        IPathProvider paths) : ITaskItemRepository
    {
        private readonly IJsonFileStore _store = store;
        private readonly IPathProvider _paths = paths;
        public async Task DeleteByIdAsync(Guid id, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(id);
            await _store.DeleteAsync(path, ct);
        }

        public async Task<bool> ExistsAsync(string title, DateTime? dueDate, CancellationToken cancellationToken)
        {
            var path = _paths.TaskItemsDirectory;
            var taskItems = await _store.ReadAllFromDirectoryAsync<TaskItem>(path, cancellationToken);

            return taskItems.Any(t =>
                t.Title == title &&
                t.DueDate == dueDate);
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(id);
            return await _store.ReadAsync<TaskItem>(path, ct);
        }

        public async Task<IReadOnlyList<TaskItem>> GetByTodoListIdAsync(Guid todoListId, CancellationToken ct)
        {
            var path = _paths.TaskItemsDirectory;

            var allTaskItems =
                await _store.ReadAllFromDirectoryAsync<TaskItem>(path, ct);

            return [.. allTaskItems.Where(t => t.TodoListId == todoListId)];
        }

        public async Task<TaskItem> SaveAsync(TaskItem taskItem, CancellationToken ct)
        {
            var path = _paths.GetTaskItemFilePath(taskItem.Id);
            await _store.WriteAsync(path, taskItem, ct);
            return taskItem;
        }
    }
}
