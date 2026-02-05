using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Domain.Entities;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem.Repositories
{
    public sealed class UserRepository(
        IJsonFileStore store,
        IPathProvider paths) : IUserRepository
    {
        private readonly IJsonFileStore _store = store;
        private readonly IPathProvider _paths = paths;

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var path = _paths.GetUserFilePath(id);
            return await _store.ReadAsync<User>(path, ct);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct)
        {
            var users = await _store.ReadAllFromDirectoryAsync<User>(_paths.UsersDirectory, ct);

            return users.FirstOrDefault(u =>
                string.Equals(u.Username?.Trim(), username.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public async Task<User> SaveAsync(User user, CancellationToken ct)
        {
            var path = _paths.GetUserFilePath(user.Id);
            await _store.WriteAsync(path, user, ct);
            return user;
        }
    }

}
