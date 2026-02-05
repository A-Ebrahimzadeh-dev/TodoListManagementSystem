namespace TodoListManagementSystem.Shared.Exceptions
{
    public class ForbiddenException : SolutionException
    {
        protected ForbiddenException(string message) : base(message, ErrorType.Forbidden)
        {
        }
    }

    public class ForbiddenException<T> : SolutionException<T>
    {
        protected ForbiddenException(string message, T detail) : base(message, ErrorType.Forbidden, detail)
        {
        }
    }
}
