using Qmera.Blazor.Modules.Accounts.Users;

namespace Qmera.Blazor.Modules.Accounts;

public static class AccountsModule
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services)
    {
        services.AddUsersModule();
        return services;
    }
}