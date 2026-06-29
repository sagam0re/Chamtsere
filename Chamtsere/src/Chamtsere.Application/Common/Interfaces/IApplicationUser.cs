namespace Chamtsere.Application.Common.Interfaces;

public interface IApplicationUser
{
    string Id { get; }
    string UserName { get; }
    string Email { get; }
    List<string>? Roles { get; }
}
