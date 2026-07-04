using Chamtsere.Application.Common.Interfaces;
using System.Security.Claims;

namespace Chamtsere.API.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Email => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "chamtsere-api/email")?.Value;
    public string? UserName => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "chamtsere-api/nickname")?.Value;
    public List<string>? Roles => _httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == "chamtsere-api/roles").Select(c => c.Value.ToString()).ToList();
    public string? ExternalAuthId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    public string? TenantId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("TenantId");

}