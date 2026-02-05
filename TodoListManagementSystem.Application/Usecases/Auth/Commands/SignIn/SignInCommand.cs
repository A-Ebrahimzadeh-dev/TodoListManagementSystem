using MediatR;

namespace TodoListManagementSystem.Application.Usecases.Auth.Commands.SignIn
{
    public record SignInCommand(string Username, string Password) : IRequest<Guid>;
}
