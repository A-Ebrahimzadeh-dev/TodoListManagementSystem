using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Domain.Abstractions.Repositories
{
    public interface ITodoListRepository
    {
        Task<IReadOnlyList<TodoList>> GetByUserIdAsync(Guid userId, CancellationToken ct);

        Task<TodoList?> GetByIdAsync(Guid userId, Guid todoListId, CancellationToken ct);

        Task<bool> ExistsAsync(Guid userId, string title, string? description, CancellationToken ct);
        
        Task<bool> ExistsAsync(Guid userId, Guid todoListId, CancellationToken ct);

        Task<TodoList> SaveAsync(TodoList todoList, CancellationToken ct);

        Task<bool> DeleteByIdAsync(Guid userId, Guid todoListId, CancellationToken ct);
    }
}
