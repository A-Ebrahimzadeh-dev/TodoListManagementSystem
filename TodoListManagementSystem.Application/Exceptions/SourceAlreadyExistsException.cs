using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Application.Exceptions
{
    public class SourceAlreadyExistsException(string message) : ConflictException(message);
}
