namespace Application.IntegrationTests.Track.Command.AddTracksCommand;

using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

public class AddTracksCommandTest
{
    [Test]
    public async Task ShouldRequiredQueryFiled()
    {
        var query = new Application.Track.Command.AddTracksCommand.AddTracksCommand(string.Empty, string.Empty);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnListOfSearchPlaylist()
    {
        var command = new Application.Track.Command.AddTracksCommand.AddTracksCommand("0123456789", "0123456789");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
    }
}
