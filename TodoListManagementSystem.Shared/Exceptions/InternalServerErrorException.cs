namespace TodoListManagementSystem.Shared.Exceptions
{
    public class InternalServerErrorException : SolutionException
    {
        protected InternalServerErrorException(string message) : base(message, ErrorType.BadRequest)
        {
        }
    }

    public class InternalServerErrorException<T> : SolutionException<T>
    {
        protected InternalServerErrorException(string message, T detail) : base(message, ErrorType.BadRequest, detail)
        {
        }
    }
}
