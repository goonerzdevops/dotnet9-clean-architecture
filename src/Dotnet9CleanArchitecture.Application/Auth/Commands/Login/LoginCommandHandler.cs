
using Dotnet9CleanArchitecture.Application.Auth.Models;
using Dotnet9CleanArchitecture.Application.Interfaces;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.Request, cancellationToken);
    }
}
