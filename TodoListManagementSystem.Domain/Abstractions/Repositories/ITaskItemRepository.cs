using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Domain.Abstractions.Repositories
{
    public interface ITaskItemRepository
    {
        Task<IReadOnlyList<TaskItem>> GetByTodoListIdAsync(Guid TodoListId, CancellationToken ct);
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct);
        Task DeleteByIdAsync(Guid id, CancellationToken ct);
        Task<TaskItem> SaveAsync(TaskItem taskItem, CancellationToken ct);
        Task<bool> ExistsAsync(string title, DateTime? dueDate, CancellationToken cancellationToken);
    }
}
