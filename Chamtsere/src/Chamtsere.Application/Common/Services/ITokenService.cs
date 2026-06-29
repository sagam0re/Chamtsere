using Chamtsere.Domain.Entities.User;
using System.Security.Claims;

namespace Chamtsere.Application.Common.Services;

public interface ITokenService
{
    Task<ITokenResult> GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles);
    Task<string> GenerateAndSaveRefreshToken(ApplicationUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
