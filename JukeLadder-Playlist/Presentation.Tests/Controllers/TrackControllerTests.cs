using FluentAssertions;
using Newtonsoft.Json;
using Presentation.Requests;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Presentation.Tests.Testing;

namespace Presentation.Tests.Controllers;
    public class TrackControllerTests
    {
    private readonly static string _controllerUrl = $"{Url}api/v{ApiVersion}/Track";
    #region Update
    [Test]
    public async Task VoteTrackWithNoRequiredDataShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new VoteTrackLikeRequest(string.Empty, string.Empty, string.Empty)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task VoteTrackShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new VoteTrackLikeRequest("1", "123456", "up")), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    #endregion

    #region Create
    [Test]
    public async Task AddTrackWithNoRequiredDataShouldReturnBadRequest()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new AddTrackPlaylistRequest(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task AddTrackShouldReturnOk()
    {
        var result = await SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_controllerUrl),
            Content = new StringContent(JsonConvert.SerializeObject(new AddTrackPlaylistRequest("KnowedId", "test", "KnowedTitle","test", "test", "test", 120)), Encoding.UTF8, "application/json")
        });

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    #endregion
}

