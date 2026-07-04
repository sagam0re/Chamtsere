using Chamtsere.API.Services;
using Chamtsere.Application.Common.Interfaces;

namespace Chamtsere.API;

public static class DependencyInjection
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUser, CurrentUser>();
    }
}
