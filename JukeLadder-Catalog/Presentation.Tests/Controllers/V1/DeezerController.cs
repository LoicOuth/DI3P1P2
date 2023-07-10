using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Presentation.Tests.Testing;

namespace Presentation.Tests.Controllers.V1;

public class DeezerController
{
    private readonly static string _controllerUrl = $"{Url}api/v1/Deezer";

    [Test]
    public async Task SearchWithNoRequiredDataShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_controllerUrl}/search/playlist")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }


    [Test]
    public async Task SearchWithGoodQueryShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_controllerUrl}/search/playlist?query=daftpunk")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

}
