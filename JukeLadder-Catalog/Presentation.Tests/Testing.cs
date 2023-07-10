using Application.Common.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Tests;

[SetUpFixture]
public partial class Testing
{
    public const string Url = "Http://localhost:7115/";
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static HttpClient _client;


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
        Environment.SetEnvironmentVariable(EnvConst.KEYCLOAK_RSA_PUBLIC_KEY, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoMfh36aTIS/hJlNsePXDg1A2OZNmRA0Y94ftwQgcGnzCDTfkqfpS5aukfQonEWfgWFBV20xp/fpWgA4LuAjAg329hza9ulet+0cvgXkwENqx1Jd/PgApUHmDaF/LRzcUG0pD5SDtcYYy1NSoCEq4CZE68PzdIALLLJHsMMW+aqmOE2WEj/LydX14YvvwT0zHBsG05g2MMieG+XTcxJnd/jDJzumgCDVySovRKRxGrk74dbbCJKKlVN5ZWJfx6eat+U0A8SfbRbFjDHJtEt6tdvhBU8deX/iNvMQ3Ywn5S1iw2XesfE20nsnrsW/B5gM2c2841hypk1GJN88+Nk37iwIDAQAB");
        Environment.SetEnvironmentVariable(EnvConst.KEYCLOAK_ISSUER, "test");
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _client = _factory.CreateClient();
    }

    public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
    {
        return await _client.SendAsync(requestMessage);
    }
}
