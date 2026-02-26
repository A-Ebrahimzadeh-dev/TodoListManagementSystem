namespace TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem
{
    public interface IPathProvider
    {
        string RootPath { get; }
        string UsersRoot { get; }
        string TodoListsRoot { get; }
        string TaskItemsRoot { get; }

        string GetUserFilePath(Guid userId);
        string GetUserTodoListsDirectory(Guid userId);
        string GetTodoListFilePath(Guid userId, Guid todoListId);
        string GetTaskItemsDirectory(Guid userId, Guid todoListId);
        string GetTaskItemFilePath(Guid userId, Guid todoListId, Guid taskItemId);
    }
}
