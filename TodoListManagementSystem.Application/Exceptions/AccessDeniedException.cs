using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Application.Exceptions
{
    public class AccessDeniedException(string Message) : ForbiddenException(Message);
}
