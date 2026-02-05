using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Domain.Abstractions.Repositories
{
    public interface ITodoListRepository
    {
        Task<IReadOnlyList<TodoList>> GetByUserIdAsync(Guid userId, CancellationToken ct);
        Task<TodoList?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<bool> ExistsAsync(string title, string? description, CancellationToken ct);
        Task<TodoList> SaveAsync(TodoList todoList, CancellationToken ct);
        Task DeleteByIdAsync(Guid id, CancellationToken ct);
    }
}