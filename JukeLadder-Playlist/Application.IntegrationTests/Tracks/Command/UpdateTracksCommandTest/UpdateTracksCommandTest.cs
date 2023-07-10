using Application.Tracks.Commands.VoteTrackCommand;
using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Tracks.Command.UpdateTracksCommandTest;
public class VoteTrackCommandTest
{
    [Test]
    public async Task ShouldRequiredQueryFiled()
    {
        var query = new VoteTrackCommand(string.Empty, string.Empty, string.Empty);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpvoteTrackPlaylist()
    {
        var command = new VoteTrackCommand("1", "123456", "up");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldDownVoteTrackPlaylist()
    {
        var command = new VoteTrackCommand("1", "123456", "down");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
    }
}

