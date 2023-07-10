using Application.Franchises.Queries.GetAllFranchiseQuery;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Franchise.Queries
{
    public class GetAllFranchiseQueryTests
    {
        [Test]
        public async Task ShoulReturnAllFranchise()
        {
            var command = new GetAllFranchiseQuery();

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }
    }
}
