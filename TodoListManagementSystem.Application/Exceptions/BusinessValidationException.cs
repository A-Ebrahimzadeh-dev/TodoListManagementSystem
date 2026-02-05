using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Application.Exceptions
{
    public class BusinessValidationException(string message) : BadRequestException(message);

    public class BusinessValidationException<TDetails>(string message, TDetails details) : BadRequestException<TDetails>(message, details);
}
