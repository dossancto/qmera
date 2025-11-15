using Microsoft.AspNetCore.Identity;

using Qmera.Blazor.Data;

namespace Qmera.Blazor.Modules.Accounts.Users.Configurations;

public static class IdentityConfiguration
{
    public static void AddUsersIdentityConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }
}