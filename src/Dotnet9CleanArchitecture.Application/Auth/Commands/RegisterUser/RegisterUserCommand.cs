
using Dotnet9CleanArchitecture.Application.Auth.Models;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.RegisterUser;

public record RegisterUserCommand(RegisterRequest Request) : IRequest<LoginResponse>;
