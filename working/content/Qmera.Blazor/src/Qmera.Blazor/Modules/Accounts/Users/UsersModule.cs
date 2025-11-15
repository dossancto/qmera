using Qmera.Blazor.Modules.Accounts.Users.Configurations;

namespace Qmera.Blazor.Modules.Accounts.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddUsersIdentityConfiguration();
        return services;
    }
}