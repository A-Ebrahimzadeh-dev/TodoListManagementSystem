using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.Application.Exceptions
{
    public class InternalServerException(string Message) : InternalServerErrorException(Message)
    {
    }
}
