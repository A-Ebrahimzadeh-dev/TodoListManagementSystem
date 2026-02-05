using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Application.Exceptions
{
    public class SourceNotFoundException(string message) : NotFoundException(message);
}
