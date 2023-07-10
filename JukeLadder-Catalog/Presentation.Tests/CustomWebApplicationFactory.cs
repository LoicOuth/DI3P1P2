using Application.Common.Interfaces;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
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
        });
    }
}