using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem
{
    public sealed class FileSystemInitializer(IPathProvider pathProvider) : IFileSystemInitializer
    {
        private readonly IPathProvider _pathProvider = pathProvider;

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            Directory.CreateDirectory(_pathProvider.RootPath);
            Directory.CreateDirectory(_pathProvider.UsersRoot);
            Directory.CreateDirectory(_pathProvider.TodoListsRoot);
            Directory.CreateDirectory(_pathProvider.TaskItemsRoot);

            await Task.CompletedTask;
        }
    }
}
