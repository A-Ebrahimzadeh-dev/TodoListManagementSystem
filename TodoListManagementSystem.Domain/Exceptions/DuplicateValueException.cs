using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Domain.Exceptions
{
    public class DuplicateValueException(string message) : ConflictException(message);
}
