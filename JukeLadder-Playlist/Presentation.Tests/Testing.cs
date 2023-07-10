using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Tests;
[SetUpFixture]
public partial class Testing
    {
    public const string Url = "http://localhost:7138/";
    public const string ApiVersion = "1";
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static HttpClient _client;


    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _client = _factory.CreateClient();
    }

    public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
    {
        return await _client.SendAsync(requestMessage);
    }
    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
       // var context = _factory.Services.GetRequiredService<IMongoDbHelper<TrackModel>>();
    }
}

