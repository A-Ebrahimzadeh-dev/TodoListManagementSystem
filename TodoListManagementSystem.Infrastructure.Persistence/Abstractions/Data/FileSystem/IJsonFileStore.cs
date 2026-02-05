using TodoListManagementSystem.Domain.Abstractions;

namespace TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem
{
    public interface IJsonFileStore
    {
        Task<T?> ReadAsync<T>(string filePath, CancellationToken cancellationToken) where T : IEntity;

        Task<IReadOnlyList<T>> ReadAllFromDirectoryAsync<T>(string directoryPath, CancellationToken cancellationToken) where T : IEntity;

        Task WriteAsync<T>(string filePath, T data, CancellationToken cancellationToken) where T : IEntity;

        Task DeleteAsync(string filePath, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(string filePath, CancellationToken cancellationToken);
    }
}
