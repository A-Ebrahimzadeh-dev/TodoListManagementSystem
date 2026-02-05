namespace TodoListManagementSystem.Shared.Exceptions
{
    public class UnauthorizedException : SolutionException
    {
        protected UnauthorizedException(string message) : base(message, ErrorType.Unauthorized)
        {

        }
    }
}
