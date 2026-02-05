using MediatR;

namespace TodoListManagementSystem.Application.Usecases.Auth.Commands.SignUp
{
    public record SignUpCommand(
        string Firstname,
        string Lastname,
        string Username,
        string Email,
        string Password) : IRequest<Guid>;
}
