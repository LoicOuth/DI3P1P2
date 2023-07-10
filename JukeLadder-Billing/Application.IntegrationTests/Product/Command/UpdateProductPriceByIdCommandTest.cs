using Application.Product.Command.UpdateProductPriceByIdCommand;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Product.Command;

public class UpdateProductPriceByIdCommandTest
{
    [Test]
    public async Task ShouldRequiredMinimumFields()
    {
        var command = new UpdateProductPriceByIdCommand(string.Empty, 0, string.Empty);

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldWork()
    {
        var command = new UpdateProductPriceByIdCommand("KnowedId", 1000, "eur");

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.ProductId.Should().Be("KnowedId");
        result.Amount.Should().Be(1000);
        result.Currency.Should().Be("eur");
    }
}
