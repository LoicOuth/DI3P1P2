using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;

namespace Application.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
            
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
            Environment.SetEnvironmentVariable("OPEN_TELEMETRY_COLLECTOR_URL", "http://localhost:4317");
        });

        builder.ConfigureServices((builder, services) =>
        {
            services.PrepareTest();
        });
    }
}
