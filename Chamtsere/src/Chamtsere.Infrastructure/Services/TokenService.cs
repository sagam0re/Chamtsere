using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Application.Common.Services;
using Chamtsere.Domain.Entities.Token;
using Chamtsere.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Chamtsere.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IChamtsereDbContext _context;
    private readonly IConfiguration _configuration;

    public TokenService(
        IChamtsereDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ITokenResult> GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"]!));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = await GenerateAndSaveRefreshToken(user);
        var tokenResult = new TokenResult(accessToken, refreshToken);
        return tokenResult;
    }

    public async Task<string> GenerateAndSaveRefreshToken(ApplicationUser user)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);
        // Save the refresh token to the database
        var tokenEntity = new Token
        {
            UserId = user.Id,
            RefreshToken = refreshToken,
            IsRevoked = false,
            ExpiryDate = DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:RefreshTokenExpiresInDays"]!))
        };
        _context.Tokens.Add(tokenEntity);
        await _context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<ITokenResult> RefreshTokenAsync(string token)
    {
        var refreshToken = await _context.Tokens.FirstOrDefaultAsync(t => t.RefreshToken == token);

        if (refreshToken is null || refreshToken.IsRevoked || refreshToken.ExpiryDate < DateTime.Now)
        {
            throw new SecurityTokenException("Invalid token");
        }


        var user = await _identityService.FindByIdAsync(refreshToken!.UserId!);
        if (user is null)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
            throw new SecurityTokenException("User Not Found.");
        }

        var roles = await _identityService.GetRolesAsync(user.Id);

        // Revoke only the used token (single-use). Other sessions remain valid.
        refreshToken.IsRevoked = true;
        await _context.SaveChangesAsync();

        // GenerateAccessToken already creates a refresh token internally
        var tokenResult = await GenerateAccessToken(user!, roles);

        return tokenResult;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
            ValidateLifetime = false // We want to get claims from expired token
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
