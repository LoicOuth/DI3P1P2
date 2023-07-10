using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Track.Queries.GetSuggestionsQuery
{
    public class GetSuggestionsQueryTest
    {
        [Test]
        public async Task ShouldRequiredQueryFiled()
        {
            var query = new Application.Track.Queries.GetSuggestionsQuery.GetSuggestionsQuery(string.Empty);

            await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnListOfuggest()
        {
            var command = new Application.Track.Queries.GetSuggestionsQuery.GetSuggestionsQuery("emi");

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }
    }
}
