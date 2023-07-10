using Microsoft.AspNetCore.Mvc.Testing;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using Application.Common.Constants;

namespace Application.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Environment.SetEnvironmentVariable(EnvConst.SOLR_URL, "http://localhost:8983/solr/tracks");
        Environment.SetEnvironmentVariable(EnvConst.SOLR_PASSWORD, "i0ljYVJDv1");
        Environment.SetEnvironmentVariable(EnvConst.SOLR_USERNAME, "admin");
        Environment.SetEnvironmentVariable(EnvConst.DEEZER_URL, "https://api.deezer.com");
        Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_USERNAME, "guest");
        Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_PASSWORD, "guest");
        Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_HOST, "localhost");
        Environment.SetEnvironmentVariable(EnvConst.RABBITMQ_VIRTUAL_HOST, "/");
        Environment.SetEnvironmentVariable(EnvConst.OPEN_TELEMETRY_COLLECTOR_URL, "http://localhost:4317");

        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}