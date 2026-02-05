namespace TodoListManagementSystem.Shared.Exceptions
{
    public class BadRequestException : SolutionException
    {
        protected BadRequestException(string message) : base(message, ErrorType.BadRequest)
        {
        }
    }

    public class BadRequestException<T> : SolutionException<T>
    {
        protected BadRequestException(string message, T detail) : base(message, ErrorType.BadRequest, detail)
        {
        }
    }
}
