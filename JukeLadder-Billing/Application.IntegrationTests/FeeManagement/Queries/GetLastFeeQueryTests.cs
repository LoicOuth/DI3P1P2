using Application.FeeManagement.Queries.GetLastFeeQuery;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.FeeManagement.Queries;

public class GetLastFeeQueryTests
{
    [Test]
    public async Task ShoulReturnLastFee()
    {
        var result = await SendAsync(new GetLastFeeQuery());

        result.Should().NotBeNull();
    }
}
