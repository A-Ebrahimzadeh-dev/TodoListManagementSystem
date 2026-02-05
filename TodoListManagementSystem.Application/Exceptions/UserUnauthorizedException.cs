using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Application.Exceptions
{
    public class UserUnauthorizedException(string message) : UnauthorizedException(message)
    {
    }
}
