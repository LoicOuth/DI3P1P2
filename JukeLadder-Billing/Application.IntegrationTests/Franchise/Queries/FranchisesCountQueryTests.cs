using Application.Franchises.Queries.FranchisesCountQuery;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Franchise.Queries;

public class FranchisesCountQueryTests
{
    [Test]
    public async Task ShoulReturnCountOfFranchises()
    {
        var result = await SendAsync(new FranchisesCountQuery());

        result.Should().BeGreaterThanOrEqualTo(0);
    }
}
