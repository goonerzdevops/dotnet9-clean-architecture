
using Dotnet9CleanArchitecture.Application.Auth.Models;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<LoginResponse>;
