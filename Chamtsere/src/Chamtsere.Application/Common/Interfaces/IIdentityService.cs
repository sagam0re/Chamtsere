using Chamtsere.Application.Common.Models;
using Chamtsere.Application.Features.UserFeature.Commands.Create;
using Chamtsere.Domain.Entities.User;

namespace Chamtsere.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<ApplicationUser?> FindByIdAsync(string userId);
    Task<string?> GetUserNameAsync(string userId);
    Task<ApplicationUser?> FindByUserNameAsync(string userName);
    Task<bool> CheckPasswordAsync(string userId, string password);

    Task<bool> IsInRoleAsync(string userId, string role);
    Task<IEnumerable<string>> GetRolesAsync(string userId);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(CreateUserCommand createUser);

    Task<Result> DeleteUserAsync(string userId);

    //string GetAuthorizedId();

    Task<Result> CreateRoleAsync(string roleName);
    Task<Result> DeleteRoleAsync(string roleName);
    Task<Result> AssignRoleToUserAsync(string userId, string roleName);
}