using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CuteChat.Persistence;
public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            if (configuration["DatabaseInMemory"] == "true")
                options.UseInMemoryDatabase("my-database");
            else
            {
                var connectionString = configuration.GetConnectionString("Default");
                options.UseNpgsql(connectionString);
            }
        });

        return services;
    }
}
