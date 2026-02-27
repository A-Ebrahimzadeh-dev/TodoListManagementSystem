using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Domain.Abstractions.Repositories
{
    public interface ITaskItemRepository
    {
        Task<IReadOnlyList<TaskItem>> GetByTodoListIdAsync(Guid userId, Guid todoListId, CancellationToken ct);

        Task<TaskItem?> GetByIdAsync(Guid userId, Guid todoListId, Guid taskItemId, CancellationToken ct);

        Task<bool> DeleteByIdAsync(Guid userId, Guid todoListId, Guid taskItemId, CancellationToken ct);

        Task<TaskItem> SaveAsync(Guid userId, TaskItem taskItem, CancellationToken ct);

        Task<bool> ExistsAsync(Guid userId, Guid todoListId, string title, DateTime? dueDate, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid userId, Guid todoListId, Guid taskItemId, CancellationToken cancellationToken);
    }
}
