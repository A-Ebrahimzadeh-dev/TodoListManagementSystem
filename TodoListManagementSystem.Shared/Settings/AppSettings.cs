namespace TodoListManagementSystem.Shared.Settings
{
    public class AppSettings
    {
        public const string SectionName = "AppSettings";
        public required JwtSettings Jwt { get; init; }
        public required StorageSettings Storage { get; init; }
        public required HashSettings Hash { get; init; }
    }
}
