namespace Presentation.Tests.Controllers;

using FluentAssertions;
using Newtonsoft.Json;
using Presentation.Requests;
using System.Text;
using static Presentation.Tests.Testing;

public class FeeManagementControllerTests
{
    private readonly static string _controllerUrl = $"{Url}api/v{ApiVersion}/FeeManagement";


    [Test]
    public async Task PostWithNoRequiredDataShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new SetFeeRequest(0, 0)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PostWithMaxGretherThanMinShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new SetFeeRequest(2000, 1000)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PostShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new SetFeeRequest(1000, 20000)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    #region GetLastFee
    [Test]
    public async Task GetLastFeeShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_controllerUrl)
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    #endregion

}
