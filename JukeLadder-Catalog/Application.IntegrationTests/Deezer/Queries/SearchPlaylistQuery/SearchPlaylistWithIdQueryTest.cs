using Application.Deezer.Queries.SearchPlaylistWithIdQuery;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Deezer.Queries.SearchPlaylistQuery;
using static Application.IntegrationTests.Testing;
public class SearchPlaylistWithIdQueryTest
{
    [Test]
    public async Task ShouldRequiredQueryFiled()
    {
        var query = new SearchPlaylistTracksWithIdQuery(string.Empty, string.Empty);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnListOfSearchPlaylist()
    {
        var command = new SearchPlaylistTracksWithIdQuery("13", "0123456789");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThan(0);
    }
}
