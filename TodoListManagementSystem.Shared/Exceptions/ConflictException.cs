namespace TodoListManagementSystem.Shared.Exceptions
{
    public class ConflictException : SolutionException
    {
        protected ConflictException(string message) : base(message, ErrorType.Conflict)
        {
        }
    }
    public class ConflictException<T> : SolutionException<T>
    {
        protected ConflictException(string message, T detail) : base(message, ErrorType.Conflict, detail)
        {
        }
    }
}
