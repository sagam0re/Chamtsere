namespace Chamtsere.Application.Common.Interfaces;

public interface ICurrentUser
{
    string? UserName { get; }
    string? Email { get; }
    string? FirstName { get; }
    string? LastName { get; }
    List<string>? Roles { get; }
    string? ExternalAuthId { get; }
    bool IsAuthenticated { get; }
    string? TenantId { get; }
}
