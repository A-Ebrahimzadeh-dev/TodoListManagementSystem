namespace TodoListManagementSystem.Shared.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public ushort AccessTokenExpirationMinutes { get; init; }
        public string Algorithm { get; init; } = "HS256";
    }

}
