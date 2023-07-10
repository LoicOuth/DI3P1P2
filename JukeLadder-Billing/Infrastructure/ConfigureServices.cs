using Application.Common.Constants;
using Infrastructure.Keycloack;
using Infrastructure.Persistance;
using Infrastructure.Stripe;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable(EnvConst.DB_CONNECTION_STRING) ?? throw new InvalidConstraintException($"ENV {EnvConst.DB_CONNECTION_STRING} NOT SET"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddSingleton<PaymentIntentService>();
        services.AddSingleton<ChargeService>();
        services.AddSingleton<RefundService>();


        services.AddSingleton<IPaymentIntentHelper, StripePaymentIntendHelper>();
        services.AddSingleton<IProductHelper, StripeProductHelper>();
        services.AddSingleton<IStripeSettings, StripeSettings>();
        services.AddSingleton<KeycloackSettings>();
        services.AddSingleton<KeycloackAuthenticationHelper>();
        services.AddSingleton<IKeycloackUsersHelper, KeycloackUsersHelper>();
        return services;
    }
}
