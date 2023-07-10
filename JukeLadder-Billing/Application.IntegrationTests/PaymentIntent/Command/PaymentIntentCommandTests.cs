using Application.PaymentIntent.Command.CreatePaymentIntentWithProductIdCommand;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.PaymentIntent.Command;

public class PaymentIntentCommandTests
{
    [Test]
    public async Task ShouldRequiredMinimumFields()
    {
        var command = new CreatePaymentIntentWithProductIdCommand(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, int.MinValue, string.Empty);

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShoulReturnClientSecret()
    {
        var command = new CreatePaymentIntentWithProductIdCommand("KnowedId", "KnowedId", "Title", "Artist", "Album", "Cover", 1, "DeezerId");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.ClientSecret.Should().Be("ClientSecret");
    }
}
