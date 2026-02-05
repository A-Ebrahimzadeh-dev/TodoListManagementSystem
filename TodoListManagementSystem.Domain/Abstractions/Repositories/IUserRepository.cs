using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<User?> GetByUsernameAsync(string username, CancellationToken ct);
        Task<User> SaveAsync(User user, CancellationToken ct);
    }
}
