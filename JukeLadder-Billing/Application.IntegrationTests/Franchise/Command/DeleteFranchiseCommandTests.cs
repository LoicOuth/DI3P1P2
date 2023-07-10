using Application.Franchises.Command.DeleteFranchiseCommand;
using Application.Franchises.Command.CreateFranchiseCommand;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Franchise.Command
{
    public class DeleteFranchiseCommandTests
    {
        [Test]
        public async Task ShouldRequiredFields()
        {
            var command = new DeleteFranchiseCommand(string.Empty);

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShoulDeleteFranchise()
        {
            var franchiseid = await SendAsync(new CreateFranchiseCommand("delteGuid", "JukkeTests"));
            var command = new DeleteFranchiseCommand(franchiseid);

            var result = await SendAsync(command);

            result.Should().NotBeNull();
        }
    }
}
