namespace Chamtsere.Application.Common.Services;

public interface ITokenResult
{
    string AccessToken { get; }
    string RefreshToken { get; }
}
