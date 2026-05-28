
using Dotnet9CleanArchitecture.Application.Auth.Models;
using Dotnet9CleanArchitecture.Application.Common.Exceptions;
using Dotnet9CleanArchitecture.Application.Interfaces;
using Dotnet9CleanArchitecture.Domain.Entities;
using Dotnet9CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Dotnet9CleanArchitecture.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _jwtSettings = new JwtSettings();
        configuration.Bind("JwtSettings", _jwtSettings);
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
            throw new DuplicateEmailException(request.Email);

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = User.Create(
            request.Email,
            request.UserName,
            passwordHash,
            request.FirstName,
            request.LastName
        );

        // Assign default "User" role
        var userRole = await _roleRepository.GetByNameAsync("User", cancellationToken);
        if (userRole != null)
            user.AddRole(userRole);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return await GenerateLoginResponseAsync(user, cancellationToken);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException();

        if (!user.IsActive)
            throw new UnauthorizedException("User account is deactivated.");

        return await GenerateLoginResponseAsync(user, cancellationToken);
    }

    public async Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

        if (user == null)
            throw new UnauthorizedException("Invalid refresh token.");

        if (!user.IsActive)
            throw new UnauthorizedException("User account is deactivated.");

        if (!_tokenService.ValidateRefreshToken(refreshToken, user.RefreshTokenExpiryTime ?? DateTime.MinValue))
            throw new UnauthorizedException("Refresh token has expired.");

        // Rotate refresh token
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        user.SetRefreshToken(newRefreshToken, refreshExpiry);
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return await GenerateLoginResponseAsync(user, cancellationToken);
    }

    public async Task RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);
        if (user != null)
        {
            user.RevokeRefreshToken();
            _userRepository.Update(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task<LoginResponse> GenerateLoginResponseAsync(User user, CancellationToken cancellationToken)
    {
        var roles = user.Roles.Select(r => r.Name).ToList();
        if (!roles.Any())
            roles = new List<string> { "User" };

        var accessToken = _tokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        user.SetRefreshToken(newRefreshToken, refreshExpiry);
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponse(
            AccessToken: accessToken,
            RefreshToken: newRefreshToken,
            ExpiresAt: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            Email: user.Email,
            UserName: user.UserName,
            Roles: roles
        );
    }
}
