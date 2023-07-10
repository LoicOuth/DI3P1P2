namespace Application.IntegrationTests.Deezer.Queries.SearchPlaylistQuery;

using Application.Deezer.Queries.SearchPlaylistQuery;
using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

public class SearchPlaylistQueryTest
{
    [Test]
    public async Task ShouldRequiredQueryFiled()
    {
        var query = new SearchPlaylistQuery(string.Empty);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnListOfSearchPlaylist()
    {
        var command = new SearchPlaylistQuery("daftpunk");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThan(0);
    }
}
