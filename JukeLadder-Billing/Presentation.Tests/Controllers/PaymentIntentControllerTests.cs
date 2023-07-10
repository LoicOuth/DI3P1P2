using FluentAssertions;
using Newtonsoft.Json;
using Presentation.Requests;
using System.Text;
using static Presentation.Tests.Testing;

namespace Presentation.Tests.Controllers;

public class PaymentIntentControllerTests
{
    private readonly static string _controllerUrl = $"{Url}api/v{ApiVersion}/PaymentIntend";

    [Test]
    public async Task PostWithNoRequiredDataShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new CreatePaymentIntentRequest(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, int.MinValue, string.Empty)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PostWithUnknowProductIdShouldReturnNotFound()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new CreatePaymentIntentRequest("UnknowId", "UnknowId", "Title", "Artist", "Album", "Cover", 1 ,"KnowedId")), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}