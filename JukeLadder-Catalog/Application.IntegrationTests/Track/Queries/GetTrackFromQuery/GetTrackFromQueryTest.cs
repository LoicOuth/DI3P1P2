using Domain.Enums;
using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Track.Queries.GetTrackFromQuery
{
    public  class GetTrackFromQueryTest
    {
        [Test]
        public async Task ShouldRequiredQueryFiled()
        {
            var query = new Application.Track.Queries.GetTrackFromQuery.GetTrackFromQuery(SolrFields.Album, string.Empty, string.Empty, string.Empty);

            await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnListOfTrack()
        {
            var command = new Application.Track.Queries.GetTrackFromQuery.GetTrackFromQuery(SolrFields.Artist, "Eminem", "Tous", "franchiseId");

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }
    }
}
