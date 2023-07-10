using Application.Franchises.Command.CreateFranchiseCommand;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Franchise.Command
{
    public class AddFranchiseCommandTests
    {
        [Test]
        public async Task ShouldRequiredFields()
        {
            var command = new CreateFranchiseCommand(string.Empty, string.Empty);

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShoulCreateFranchise()
        {
            var command = new CreateFranchiseCommand("createGuid","JukkeTests");

            var result = await SendAsync(command);

            result.Should().NotBeNull();
        }
    }
}
