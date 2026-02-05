using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Domain.Exceptions
{
    public class ArgumentNullOrEmptyException(string message) : BadRequestException(message);
}
