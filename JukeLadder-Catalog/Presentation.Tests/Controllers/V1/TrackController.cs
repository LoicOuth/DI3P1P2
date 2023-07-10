using System;
using System.Net.Http;
using static Presentation.Tests.Testing;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Presentation.Request;

namespace Presentation.Tests.Controllers.V1
{
    public class TrackController
    {
        private readonly static string _controllerUrl = $"{Url}api/v1/Track";
        [Test]
        public async Task GetTracksOkBadRequest()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_controllerUrl}")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetTracksOk()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_controllerUrl}?field=Artist&value=Eminem&genre=Tous&franchiseId=franchiseId")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Test]
        public async Task SuggestBadRequest()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_controllerUrl}/suggestions")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Test]
        public async Task SuggestOk()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_controllerUrl}/suggestions?query=emi")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task AddTracksBadRequest()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_controllerUrl}"),
                Content = new StringContent(JsonConvert.SerializeObject(new AddTracksRequest("","")), Encoding.UTF8, "application/json")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Test]
        public async Task AddTracksOk()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_controllerUrl}"),
                Content = new StringContent(JsonConvert.SerializeObject(new AddTracksRequest("0123456789", "1231")), Encoding.UTF8, "application/json")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
