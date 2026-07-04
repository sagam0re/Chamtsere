using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Application.Common.Models;
using Chamtsere.Application.Features.UserFeature.Commands.Create;
using Chamtsere.Application.Features.UserFeature.Queries.UserList;
using Chamtsere.Domain.Entities.User;
using Chamtsere.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chamtsere.Infrastructure.Identity;


public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly ChamtsereDbContext _dbContext;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        ChamtsereDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _dbContext = dbContext;
    }

    public IQueryable<UserListQueryResult>? GetAllAsync()
    {
        return _userManager.Users.Where(u => u != null)
                                      .AsNoTracking()
                                      .Select(u => new UserListQueryResult
                                      {
                                          Id = u.Id,
                                          UserName = u.UserName!,
                                          FirstName = u.FirstName,
                                          LastName = u.LastName,
                                          Email = u.Email!,
                                          PhoneNumber = u.PhoneNumber!,
                                          Roles = _dbContext.UserRoles
                                              .Where(ur => ur.UserId == u.Id)
                                              .Join(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name!)
                                              .ToList()
                                      })
                                      .OrderBy(u => u.UserName);
    }
    public async Task<ApplicationUser?> FindByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }
    public async Task<ApplicationUser?> FindByUserNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
    public async Task<bool> CheckPasswordAsync(string userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.CheckPasswordAsync(user, password);
    }

    /* public string GetAuthorizedId()
     {
         var userClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         return userClaim ?? string.Empty;
     }*/

    public async Task<(Result Result, string UserId)> CreateUserAsync(CreateUserCommand createUser)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        var user = new ApplicationUser
        {
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            UserName = createUser.UserName,
            Email = createUser.Email,
        };

        var result = await _userManager.CreateAsync(user, createUser.Password);
        await _userManager.AddToRolesAsync(user, createUser.Roles.Select(r => r.ToString()));
        await transaction.CommitAsync();
        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<IEnumerable<string>> GetRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return await _userManager.GetRolesAsync(user!);

    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success(null);
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    // Role management methods can be added here if needed, such as creating roles, assigning roles to users, etc.
    public async Task<Result> CreateRoleAsync(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
        {
            return Result.Failure([$"Role '{roleName}' already exists."]);
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        return result.ToApplicationResult();
    }

    public async Task<Result> DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result.Failure([$"Role '{roleName}' not found."]);
        }
        var result = await _roleManager.DeleteAsync(role);
        return result.ToApplicationResult();
    }

    public async Task<Result> AssignRoleToUserAsync(string userId, IEnumerable<string> roleNames)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure([$"User with ID '{userId}' not found."]);
        }
        foreach (var roleName in roleNames)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return Result.Failure([$"Role '{roleName}' does not exist."]);
            }
        }

        var result = await _userManager.AddToRolesAsync(user, roleNames);
        return result.ToApplicationResult();
    }
}