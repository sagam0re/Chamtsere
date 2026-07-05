using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Infrastructure.Data;
using Chamtsere.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chamtsere.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        // Register the interceptors
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, SoftDeleteInterceptor>();

        // Register the DbContext and its interface
        builder.Services.AddDbContext<ChamtsereDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("ChamtsereDbConnection"))
                .AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        builder.Services.AddScoped<IChamtsereDbContext>(provider => provider.GetRequiredService<ChamtsereDbContext>());
    }
}
