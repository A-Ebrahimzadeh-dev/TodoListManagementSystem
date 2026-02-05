namespace TodoListManagementSystem.Shared.Exceptions
{
    public class SolutionException : Exception, ISolutionException
    {
        protected SolutionException(string message, ErrorType errorType)
        {
            Message = message;
            ErrorType = errorType;
        }

        public override string Message { get; }
        public ErrorType ErrorType { get; }
    }

    public class SolutionException<T> : SolutionException, ISolutionException<T>
    {
        protected SolutionException(
            string message,
            ErrorType errorType,
            T detail = default!) : base(message, errorType)
        {
            Detail = detail;
        }

        public T Detail { get; }
    }
}
