using MediatR;
using TodoListManagementSystem.Application.Abstractions.Utilities;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.Auth.Commands.SignIn
{
    public class SignInCommandHandler(
    IUserRepository userRepository,
    IHashManager hashManager) : IRequestHandler<SignInCommand, Guid>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHashManager _hashManager = hashManager;

        public async Task<Guid> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(command.Username, cancellationToken)
                ?? throw new UserUnauthorizedException("Invalid login credentials");

            if (!_hashManager.Verify(existingUser.Password, command.Password))
                throw new UserUnauthorizedException("Invalid login credentials");

            return existingUser.Id;
        }
    }

}
