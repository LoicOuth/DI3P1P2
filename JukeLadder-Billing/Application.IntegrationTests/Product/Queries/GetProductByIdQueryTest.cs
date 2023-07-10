using Application.Product.Queries.GetProductByIdQuery;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Product.Queries;

public class GetProductByIdQueryTest
{
    [Test]
    public async Task ShoulReturnOneProductByIdRequiredQueryFiled()
    {
        var query = new GetProductByIdQuery(string.Empty);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShoulReturnOneProductById()
    {
        var query = new GetProductByIdQuery("KnowedId");
        var result = await SendAsync(query);

        result.Should().NotBeNull();
    }
}
