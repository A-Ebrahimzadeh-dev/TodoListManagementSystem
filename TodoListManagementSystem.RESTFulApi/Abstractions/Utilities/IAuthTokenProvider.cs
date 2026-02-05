namespace TodoListManagementSystem.RESTFulApi.Abstractions.Utilities
{
    public interface IAuthTokenProvider
    {
        string GenerateToken(Guid userId, DateTime? expireDate);
    }
}
