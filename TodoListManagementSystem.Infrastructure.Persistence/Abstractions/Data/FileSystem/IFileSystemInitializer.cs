namespace TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem
{
    public interface IFileSystemInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken = default);
    }
}

