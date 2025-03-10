using CuteChat.Infrastructure.Hubs.MessagesStorage;
using CuteChat.Infrastructure.Security;
using CuteChat.Infrastructure.Security.Jwt;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CuteChat.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AccountSettings>(configuration.GetSection("Account"));
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.AddScoped<IJwtService, JwtService>();
        services.AddTransient<IMessageManager, StoreMessageJob>();

        var hangfireConnectionString = configuration.GetConnectionString("Hangfire");

        services.AddHangfire(config => 
        {
            config.UsePostgreSqlStorage(settings =>
            {
                settings.UseNpgsqlConnection(hangfireConnectionString);
            });
        });

        services.AddHangfireServer();

        var jwtSection = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection.GetValue<string>("SecretKey") ?? string.Empty);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSection.GetValue<string>("Issuer") ?? string.Empty,
                ValidAudience = jwtSection.GetValue<string>("Audience") ?? string.Empty,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddAuthorization();

        return services;
    }
}
