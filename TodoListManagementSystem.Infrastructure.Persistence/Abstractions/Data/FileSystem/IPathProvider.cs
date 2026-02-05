namespace TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem
{
    public interface IPathProvider
    {
        string RootPath { get; }

        string UsersDirectory { get; }
        string TaskItemsDirectory { get; }
        string TodoListsDirectory { get; }

        string GetUserFilePath(Guid userId);
        string GetTaskItemFilePath(Guid taskId);
        string GetTodoListFilePath(Guid todoListId);
    }

}
