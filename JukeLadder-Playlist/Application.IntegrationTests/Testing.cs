using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        Environment.SetEnvironmentVariable("MONGODB_CONNECTION_STRING", "mongodb://localhost:27017");
        Environment.SetEnvironmentVariable("MONGODB_DATABASE_NAME", "JukkeLadder");
        Environment.SetEnvironmentVariable("MONGODB_COLLECTION_NAME", "Playlist");
        Environment.SetEnvironmentVariable("DEEZER_URL", "https://api.deezer.com");
        Environment.SetEnvironmentVariable("RABBITMQ_USERNAME", "guest");
        Environment.SetEnvironmentVariable("RABBITMQ_PASSWORD", "guest");
        Environment.SetEnvironmentVariable("RABBITMQ_HOST", "localhost");
        Environment.SetEnvironmentVariable("RABBITMQ_VIRTUAL_HOST", "/");
        Environment.SetEnvironmentVariable("KEYCLOAK_ISSUER", "http://localhost:12916/realms/JukeLadder");
        Environment.SetEnvironmentVariable("KEYCLOAK_RSA_PUBLIC_KEY", "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA/DknDFER5mkTuY9Ha/FY7d65n/lAbNuGjF5np2Wyg+vSiiglkIONVgSmGNTlKuqrTpyrhoiyWf0XeJAipphH32fVUHuRkDHAVhKOmBgeKmlWsmco+N37QHHgu1CpxyvYuip7sJmDdyYQb0lxyzvqa10LnJCdWrD0eooAo3OPfNPxk2lbHKf3uvY1PTPE5GR/Mi5VZRSYP+VpWWrBcdU5DRYOKlW+XWhm90r/PR+010Nd49MMmGhgD/l3zYlbrhkycahJyDt+M2vHWI7X7CrZgPMom8SRiqfcO/Iw/tu1HSpqR42FJoyga4E6epsDTiPR0wHHNLBif99O0n/35WyaLwIDAQAB");
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}