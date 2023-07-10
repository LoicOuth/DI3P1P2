using Application.Franchises.Command.UpdateFranchiseCommand;
using Application.Franchises.Command.CreateFranchiseCommand;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Franchise.Command
{
    public class UpdateFranchiseCommandTests
    {
        [Test]
        public async Task ShouldRequiredFields()
        {
            var command = new UpdateFranchiseCommand(string.Empty, string.Empty, string.Empty, string.Empty);

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShoulUpdateFranchise()
        {
            var franchiseid = await SendAsync(new CreateFranchiseCommand("updateGuid", "JukkeTests"));

            franchiseid.Should().NotBeNull();

            var command = new UpdateFranchiseCommand(franchiseid, "User1", "JukkeTests", "Tous");

            var result = await SendAsync(command);

            result.Should().NotBeNull();
        }
    }
}
