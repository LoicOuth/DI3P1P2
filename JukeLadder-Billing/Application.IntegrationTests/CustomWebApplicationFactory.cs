﻿using Application.Common.Constants;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;

class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);

            Environment.SetEnvironmentVariable(EnvConst.DB_CONNECTION_STRING, "test");
            Environment.SetEnvironmentVariable(EnvConst.KEYCLOAK_ISSUER, "test");
            Environment.SetEnvironmentVariable(EnvConst.KEYCLOAK_RSA_PUBLIC_KEY, "test");
            Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_USERNAME, "guest");
            Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_PASSWORD, "guest");
            Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_HOST, "localhost");
            Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_VIRTUAL_HOST, "/");
            Environment.SetEnvironmentVariable(EnvConst.STRIPE_PRODUCT_DEMOTE_ID, "prod_MSwUWCD74YEaGd");
            Environment.SetEnvironmentVariable(EnvConst.STRIPE_PRODUCT_PROMOTE_ID, "prod_MSwTAmtUmsxQ9A");
            Environment.SetEnvironmentVariable(EnvConst.STRIPE_PUBLISHABLE_KEY, "pk_test_51LdY9UJ7HRQaIjtYrLL7PkTUw0GLtC9GvKWWNNK9WxW9RZ0O5qquTR8yEhCiORh5SbnlyK5lZJDXl0ixL6sEcSDI00yfsUvZUw");
            Environment.SetEnvironmentVariable(EnvConst.STRIPE_SECRET_KEY, "sk_test_51LdY9UJ7HRQaIjtYTzkit3g9FPHzlJ7MHosudvdJ478Rgk51TCMeU3KTojcxutRqNr28lPnZGEXBzMDnoeJqJpgP0092g2oMBa");
            Environment.SetEnvironmentVariable(EnvConst.STRIPE_WEBHOOK_SECRET, "unknow");
            Environment.SetEnvironmentVariable(EnvConst.OPEN_TELEMETRY_COLLECTOR_URL, "http://localhost:4317");
        });

        builder.ConfigureServices((builder, services) =>
        {
            services.PrepareTest();
            services.Remove<DbContextOptions<ApplicationDbContext>>()
            .AddDbContext<ApplicationDbContext>((sp, options) =>
                options.UseInMemoryDatabase("TestDb"));
            services
            .Remove<IApplicationDbContext>()
            .AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            var context = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            context.FeeParameters.Add(new FeeParameter(long.MinValue, long.MaxValue, true, DateTime.Now, DateTime.Now));
            context.SaveChangesAsync(CancellationToken.None).Wait();
        });
    }
}
