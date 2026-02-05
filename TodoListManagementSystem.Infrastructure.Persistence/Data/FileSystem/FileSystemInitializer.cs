using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem
{
    public sealed class FileSystemInitializer(IPathProvider pathProvider) : IFileSystemInitializer
    {
        private readonly IPathProvider _pathProvider = pathProvider;

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var directories = new[]
            {
                _pathProvider.RootPath,
                _pathProvider.UsersDirectory,
                _pathProvider.TaskItemsDirectory,
                _pathProvider.TodoListsDirectory
            };

            foreach (var dir in directories)
            {
                Directory.CreateDirectory(dir);
            }

            await Task.CompletedTask;
        }
    }
}
