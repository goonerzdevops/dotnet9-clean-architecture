
using Dotnet9CleanArchitecture.Application.Auth.Models;
using Dotnet9CleanArchitecture.Application.Common.Exceptions;
using Dotnet9CleanArchitecture.Application.Interfaces;
using Dotnet9CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.Request, cancellationToken);
    }
}
