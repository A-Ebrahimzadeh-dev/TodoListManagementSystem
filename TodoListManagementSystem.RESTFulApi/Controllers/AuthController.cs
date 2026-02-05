using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TodoListManagementSystem.Application.Usecases.Auth.Commands.SignIn;
using TodoListManagementSystem.Application.Usecases.Auth.Commands.SignUp;
using TodoListManagementSystem.RESTFulApi.Abstractions.Utilities;
using TodoListManagementSystem.Shared.Settings;

namespace TodoListManagementSystem.RESTFulApi.Controllers
{
    public class AuthController(
        IMediator mediator,
        IOptionsMonitor<AppSettings> options,
        IAuthTokenProvider authTokenProvider) : BaseController
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAuthTokenProvider _authTokenProvider = authTokenProvider;
        private readonly JwtSettings _jwtSettings = options.CurrentValue.Jwt;

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUpAsync([FromBody] SignUpCommand request)
        {
            var userId = await _mediator.Send(request);
            var expiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var accessToken = _authTokenProvider.GenerateToken(userId, expiryDate);
            return Created(
                $"/api/users/{userId}",
                new { userId, accessToken, expiresAt = expiryDate }
            );
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult> Post([FromBody] SignInCommand request)
        {
            var userId = await _mediator.Send(request);
            var expiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var accessToken = _authTokenProvider.GenerateToken(userId, expiryDate);
            return Ok(new { userId, accessToken, expiresAt = expiryDate });
        }
    }
}
