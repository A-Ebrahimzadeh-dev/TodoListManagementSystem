using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Domain.Exceptions
{
    public class ValueNotFoundException(string message) : NotFoundException(message);
}
