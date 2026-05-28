
namespace Dotnet9CleanArchitecture.Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Invalid credentials.") { }
    public UnauthorizedException(string message) : base(message) { }
}
