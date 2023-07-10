namespace Application.IntegrationTests.Tracks.Command.AddTracksCommandTest;

using Application.Tracks.Commands.AddTrackPlaylistCommand;
using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

public class AddTracksCommandTest
{
    [Test]
    public async Task ShouldRequiredQueryFiled()
    {
        var query = new AddTrackPlaylistCommand(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnListOfSearchPlaylist()
    {
        var command = new AddTrackPlaylistCommand("test", "test", "test", "test", "test", "test", 1);

        var result = await SendAsync(command);

        result.Should().NotBeNull();
    }
}