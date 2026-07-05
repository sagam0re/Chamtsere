using Chamtsere.API.Services;
using Chamtsere.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Chamtsere.API;

public static class DependencyInjection
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUser, CurrentUser>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ReactApp", policy =>
            {
                policy.WithOrigins("http://localhost:3001")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{builder.Configuration["Authentication:Auth0:Domain"]}/";
            options.Audience = builder.Configuration["Authentication:Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,

                RoleClaimType = "chamtsere-api/roles"
            };
        });
    }
}
