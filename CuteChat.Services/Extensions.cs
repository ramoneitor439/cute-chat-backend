using CuteChat.Services.AppUser;
using CuteChat.Services.Contact;
using CuteChat.Services.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace CuteChat.Services;
public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<AppUserManager>();
        services.AddScoped<ContactManager>();
        services.AddScoped<MessageManager>();

        return services;
    }
}
