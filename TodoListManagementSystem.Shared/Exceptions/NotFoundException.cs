namespace TodoListManagementSystem.Shared.Exceptions
{
    public class NotFoundException : SolutionException
    {
        protected NotFoundException(string message) : base(message, ErrorType.NotFound)
        {
        }
    }

    public class NotFoundException<T> : SolutionException<T>
    {
        protected NotFoundException(string message, T detail) : base(message, ErrorType.NotFound, detail)
        {
        }
    }
}
