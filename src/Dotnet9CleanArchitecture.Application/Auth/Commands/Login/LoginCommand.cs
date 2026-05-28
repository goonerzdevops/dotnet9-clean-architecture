
using Dotnet9CleanArchitecture.Application.Auth.Models;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.Login;

public record LoginCommand(LoginRequest Request) : IRequest<LoginResponse>;
