using FluentAssertions;
using Newtonsoft.Json;
using Presentation.Requests;
using System.Text;
using static Presentation.Tests.Testing;

namespace Presentation.Tests.Controllers;

public class ProductControllerTests
{
    private readonly static string _controllerUrl = $"{Url}api/v{ApiVersion}/Product";

    [Test]
    public async Task PutWithNotRequierdDataShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"{_controllerUrl}/KnowedId"),
            Content = new StringContent(JsonConvert.SerializeObject(new UpdateProductPriceRequest(0, "")), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PutWithUnknowProductIdShouldReturnNotFound()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"{_controllerUrl}/UnknowId"),
            Content = new StringContent(JsonConvert.SerializeObject(new UpdateProductPriceRequest(1000, "eur")), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task PutShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"{_controllerUrl}/KnowedId"),
            Content = new StringContent(JsonConvert.SerializeObject(new UpdateProductPriceRequest(1000, "eur")), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task GetWithUnknowProductIdShouldReturnNotFound()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_controllerUrl}/UnknowId"),
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetOneShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_controllerUrl}/KnowedId"),
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

}
