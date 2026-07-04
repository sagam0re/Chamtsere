using Chamtsere.API.Services;
using Chamtsere.Application.Common.Interfaces;

namespace Chamtsere.API;

public static class DependencyInjection
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<IUser, CurrentUser>();
    }
}
