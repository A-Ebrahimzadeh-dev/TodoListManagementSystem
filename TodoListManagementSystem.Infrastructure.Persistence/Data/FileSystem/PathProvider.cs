using Microsoft.Extensions.Options;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;
using TodoListManagementSystem.Shared.Settings;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem
{
    public sealed class PathProvider : IPathProvider
    {
        private readonly StorageSettings _storageSettings;

        public PathProvider(IOptionsMonitor<AppSettings> options)
        {
            _storageSettings = options.CurrentValue.Storage;
            RootPath = Path.Combine(
                AppContext.BaseDirectory,
                _storageSettings.RootPath ?? "AppData"
            );

            UsersDirectory = Path.Combine(RootPath, _storageSettings.UsersFolder ?? "Users");
            TaskItemsDirectory = Path.Combine(RootPath, _storageSettings.TaskItemsFolder ?? "TaskItems");
            TodoListsDirectory = Path.Combine(RootPath, _storageSettings.TodoListsFolder ?? "TodoLists");
        }

        public string RootPath { get; }

        public string UsersDirectory { get; }
        public string TaskItemsDirectory { get; }
        public string TodoListsDirectory { get; }

        public string GetUserFilePath(Guid userId)
            => Path.Combine(UsersDirectory, $"{userId}.json");

        public string GetTaskItemFilePath(Guid taskId)
            => Path.Combine(TaskItemsDirectory, $"{taskId}.json");

        public string GetTodoListFilePath(Guid todoListId)
            => Path.Combine(TodoListsDirectory, $"{todoListId}.json");
    }
}
