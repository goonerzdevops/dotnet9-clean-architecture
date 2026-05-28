
using Dotnet9CleanArchitecture.Application.Auth.Models;
using Dotnet9CleanArchitecture.Application.Interfaces;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RefreshTokenAsync(request.Request.RefreshToken, cancellationToken);
    }
}
