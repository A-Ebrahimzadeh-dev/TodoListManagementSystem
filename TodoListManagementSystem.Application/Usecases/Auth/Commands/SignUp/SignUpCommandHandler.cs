using MediatR;
using TodoListManagementSystem.Application.Abstractions.Utilities;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Domain.Entities;

namespace TodoListManagementSystem.Application.Usecases.Auth.Commands.SignUp
{
    public class SignUpCommandHandler(
        IUserRepository userRepository,
        IHashManager hashManager) : IRequestHandler<SignUpCommand, Guid>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHashManager _hashManager = hashManager;

        public async Task<Guid> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(command.Username, cancellationToken);

            if (existingUser != null)
                throw new SourceAlreadyExistsException($"Username '{command.Username}' is already taken.");

            var hashedPassword = _hashManager.Create(command.Password);

            var user = User.CreateNewUser(
                command.Firstname,
                command.Lastname,
                command.Username,
                command.Email,
                hashedPassword
            );

            await _userRepository.SaveAsync(user, cancellationToken);

            return user.Id;
        }
    }
}
