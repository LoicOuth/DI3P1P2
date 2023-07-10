using Application.Common.Behaviours;
using Application.Common.Constants;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper
            (Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable(EnvConst.RABBITMQ_HOST) ?? throw new InvalidConstraintException($"ENV {EnvConst.RABBITMQ_HOST} NOT SET"), Environment.GetEnvironmentVariable(EnvConst.RABBITMQ_VIRTUAL_HOST) ?? throw new InvalidConstraintException($"ENV {EnvConst.RABBITMQ_VIRTUAL_HOST} NOT SET"), h =>
                {
                    h.Username(Environment.GetEnvironmentVariable(EnvConst.RABBITMQ_USERNAME) ?? throw new InvalidConstraintException($"ENV {EnvConst.RABBITMQ_USERNAME} NOT SET"));
                    h.Password(Environment.GetEnvironmentVariable(EnvConst.RABBITMQ_PASSWORD) ?? throw new InvalidConstraintException($"ENV {EnvConst.RABBITMQ_PASSWORD} NOT SET"));
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
