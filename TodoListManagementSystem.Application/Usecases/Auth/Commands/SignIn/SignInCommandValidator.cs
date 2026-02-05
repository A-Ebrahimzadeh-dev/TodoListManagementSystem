using FluentValidation;

namespace TodoListManagementSystem.Application.Usecases.Auth.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.")
                .Matches(@"^[a-zA-Z0-9._-]+$").WithMessage("Username can only contain letters, numbers, dots, underscores, and hyphens.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
                .Matches(@"[!@#]+").WithMessage("Password must contain at least one special character (!, @, or #).");
        }
    }
}
