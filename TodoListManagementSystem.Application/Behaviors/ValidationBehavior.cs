using FluentValidation;
using MediatR;
using TodoListManagementSystem.Application.Exceptions;

namespace TodoListManagementSystem.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationFailures
                .Where(r => !r.IsValid)
                .SelectMany(r => r.Errors)
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToList();

            if (errors.Count != 0)
                throw new BusinessValidationException<IEnumerable<string>>(
                    "Business validation exception",
                    errors);

            return await next(cancellationToken);
        }
    }
}
