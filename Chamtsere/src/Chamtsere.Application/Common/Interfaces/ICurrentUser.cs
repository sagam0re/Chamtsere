namespace Chamtsere.Application.Common.Interfaces;

public interface ICurrentUser
{
    string? Id { get; }
    string? UserName { get; }
    string? Email { get; }
    List<string>? Roles { get; }
    string? ExternalAuthId { get; }
    bool IsAuthenticated { get; }
    string? TenantId { get; }
}
