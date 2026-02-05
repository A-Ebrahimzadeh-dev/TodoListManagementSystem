using FluentValidation;

namespace TodoListManagementSystem.Application.Usecases.Auth.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Firstname is required.")
                .MaximumLength(50).WithMessage("Firstname cannot exceed 50 characters.");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Lastname is required.")
                .MaximumLength(50).WithMessage("Lastname cannot exceed 50 characters.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.")
                .Matches(@"^[a-zA-Z0-9._-]+$").WithMessage("Username can only contain letters, numbers, dots, underscores, and hyphens.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

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
