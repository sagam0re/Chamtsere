using Chamtsere.Application.Common.Services;

namespace Chamtsere.Infrastructure.Services;

public class TokenResult(string accessToken, string refreshToken) : ITokenResult
{
    public string AccessToken { get; } = accessToken;
    public string RefreshToken { get; } = refreshToken;
}
