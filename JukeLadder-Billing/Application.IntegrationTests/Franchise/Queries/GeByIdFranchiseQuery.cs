using Application.Franchises.Command.CreateFranchiseCommand;
using Application.Franchises.Queries.GetFranchiseByIdQuery;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Franchise.Queries;

public class GeByIdFranchiseQuery
{
    [Test]
    public async Task ShoulReturnAllTrackByFranchiseIdRequiredQueryFiled()
    {
        var query = new GetFranchiseByIdQuery(string.Empty);

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }
    [Test]
    public async Task ShoulReturnAllTrackByFranchiseId()
    {
        var command2 = new CreateFranchiseCommand("KnowedI", "Test");
        var result2 = await SendAsync(command2);
        
        var command = new GetFranchiseByIdQuery(result2);
        var result = await SendAsync(command);

        result.Should().NotBeNull();
    }
}
