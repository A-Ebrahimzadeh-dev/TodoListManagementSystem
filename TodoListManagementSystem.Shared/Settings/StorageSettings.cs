namespace TodoListManagementSystem.Shared.Settings
{
    public class StorageSettings
    {
        public string? RootPath { get; init; } = null;

        public string? UsersFolder { get; init; } = null;
        public string? TaskItemsFolder { get; init; } = null;
        public string? TodoListsFolder { get; init; } = null;
    }
}