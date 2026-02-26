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

            UsersRoot = Path.Combine(RootPath, "Users");
            TodoListsRoot = Path.Combine(RootPath, "TodoLists");
            TaskItemsRoot = Path.Combine(RootPath, "TaskItems");
        }

        public string RootPath { get; }
        public string UsersRoot { get; }
        public string TodoListsRoot { get; }
        public string TaskItemsRoot { get; }

        public string GetUserFilePath(Guid userId)
            => Path.Combine(UsersRoot, $"{userId}.json");

        public string GetUserTodoListsDirectory(Guid userId)
            => Path.Combine(TodoListsRoot, userId.ToString());

        public string GetTodoListFilePath(Guid userId, Guid todoListId)
            => Path.Combine(GetUserTodoListsDirectory(userId), $"{todoListId}.json");

        public string GetTaskItemsDirectory(Guid userId, Guid todoListId)
            => Path.Combine(TaskItemsRoot, userId.ToString(), todoListId.ToString());

        public string GetTaskItemFilePath(Guid userId, Guid todoListId, Guid taskItemId)
            => Path.Combine(GetTaskItemsDirectory(userId, todoListId), $"{taskItemId}.json");
    }
}
