namespace TodoListManagementSystem.Shared.Exceptions
{
    public interface ISolutionException
    {
        ErrorType ErrorType { get; }
        string Message { get; }
    }
    public interface ISolutionException<out T> : ISolutionException
    {
        T Detail { get; }
    }
}
