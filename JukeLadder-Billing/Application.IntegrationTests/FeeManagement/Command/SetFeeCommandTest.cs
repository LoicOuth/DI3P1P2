using Application.FeeManagement.Command.SetFeeCommand;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.FeeManagement.Command;

public class SetFeeCommandTest
{
    [Test]
    public async Task ShouldRequiredMinimumFields()
    {
        var command = new SetFeeCommand(0, 0);

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task MaxShouldBeGretherThanMin()
    {
        var command = new SetFeeCommand(3000, 1000);

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnNewGuid()
    {
        var command = new SetFeeCommand(1000, 3000);

        var result = await SendAsync(command);

        result.Should().NotBeNull();
    }

}
