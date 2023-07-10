using FluentAssertions;
using Newtonsoft.Json;
using Presentation.Requests;
using System.Text;
using static Presentation.Tests.Testing;

namespace Presentation.Tests.Controllers
{
    public class FranchiseControllerTests
    {
        private readonly static string _controllerUrl = $"{Url}api/v{ApiVersion}/Franchise";
        #region Create
        [Test]
        public async Task PostWithNoRequiredDataShouldReturnBadRequest()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_controllerUrl),
                Content = new StringContent(JsonConvert.SerializeObject(new CreateFranchiseRequest("", "")), Encoding.UTF8, "application/json")
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
                Content = new StringContent(JsonConvert.SerializeObject(new CreateFranchiseRequest("KnowedId", "KnowName")), Encoding.UTF8, "application/json")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        #endregion

        #region Update
        [Test]
        public async Task UpdateWithNoRequiredDataShouldReturnBadRequest()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_controllerUrl}/unknowId"),
                Content = new StringContent(JsonConvert.SerializeObject(new UpdateFranchiseRequest(String.Empty, String.Empty, string.Empty)), Encoding.UTF8, "application/json")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UpdateShouldReturnOk()
        {
            var c = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_controllerUrl),
                Content = new StringContent(JsonConvert.SerializeObject(new CreateFranchiseRequest("KnowedId", "KnowName")), Encoding.UTF8, "application/json")
            });

           var id = c.Content.ReadAsStringAsync().Result; 

            id.Should().NotBeNull();

            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_controllerUrl}/{id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new UpdateFranchiseRequest("KnowedId", "coucou", "knowedTheme")), Encoding.UTF8, "application/json")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        #endregion

        #region Delete
        [Test]
        public async Task DeleteWithUnknowIdShouldReturnBadRequest()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_controllerUrl}/null")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteShouldReturnOk()
        {
            var c = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_controllerUrl),
                Content = new StringContent(JsonConvert.SerializeObject(new CreateFranchiseRequest("KnowedId", "KnowName")), Encoding.UTF8, "application/json")
            });

            var id = c.Content.ReadAsStringAsync().Result;

            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_controllerUrl}/{id}"),
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        #endregion

        #region GetAll
        [Test]
        public async Task GetAllShouldReturnOk()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_controllerUrl)
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        #endregion

        #region GetById
        [Test]
        public async Task GetByIdShouldReturnNoFound()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_controllerUrl}/UnKnowedId")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        #endregion

        #region count
        [Test]
        public async Task CountShouldReturnOk()
        {
            var result = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_controllerUrl}/count")
            });

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        #endregion
    }
}
