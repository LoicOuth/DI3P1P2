using Application.Track.Dto;
using MassTransit;
using MassTransit.Mediator;
using Newtonsoft.Json;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.Impl;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static RabbitMQ.Client.Logging.RabbitMqClientEventSource;

namespace Infrastructure.Solr;

public class SolrHelper : ISolrHelper
{
    private readonly ISolrOperations<TrackSolrDto> _solrConnection;
    private readonly SolrSettings _solrSettings;
    private readonly HttpClient _httpClient;

    public SolrHelper(ISolrOperations<TrackSolrDto> solrConnection, SolrSettings solrSettings)
    {
        _solrConnection = solrConnection;
        _solrSettings = solrSettings;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _solrSettings.GetCredentialBase64());
    }

    public async Task<List<TrackSolrDto>> AddTracks(List<TrackSolrDto> trackSolrs, CancellationToken cancellationToken = default)
    {
        var response = await _solrConnection.AddRangeAsync(trackSolrs);
        await _solrConnection.CommitAsync();

        return trackSolrs;
    }

    public async Task<List<SuggestionSolrDto>> GetSuggestions(string query, CancellationToken cancellationToken = default)
    {
        string request = $"{_solrSettings.Url}/suggest?suggest.dictionary=suggest&suggest.dictionary=artSuggest&suggest.dictionary=albSuggest&suggest.q={query}";

        HttpResponseMessage result = await _httpClient.GetAsync(request, cancellationToken);

        if (!result.IsSuccessStatusCode)
            throw new HttpRequestException("Error connection on solr connection !");

        string stringResponse = await result.Content.ReadAsStringAsync(cancellationToken);
        var suggest = JsonDocument.Parse(stringResponse).RootElement.GetProperty("suggest");
        var alb = JsonConvert.DeserializeObject<List<SuggestionSolrDto>>(suggest.GetProperty("albSuggest").GetProperty(query).GetProperty("suggestions").ToString())!;
        alb.ForEach(x => x.Type = "album");
        var art = JsonConvert.DeserializeObject<List<SuggestionSolrDto>>(suggest.GetProperty("artSuggest").GetProperty(query).GetProperty("suggestions").ToString())!;
        art.ForEach(x => x.Type = "artist");
        var title = JsonConvert.DeserializeObject<List<SuggestionSolrDto>>(suggest.GetProperty("suggest").GetProperty(query).GetProperty("suggestions").ToString())!;
        title.ForEach(x => x.Type = "title");


        return alb.Union(art).Union(title).OrderBy(x => x.Weight).Distinct().ToList();
    }

    public async Task<List<TrackSolrDto>> QueryableTracks(string field, string value, string? themeFranchise, string franchiseId, CancellationToken cancellationToken = default)
    {
        QueryOptions options = new QueryOptions();
        options.AddOrder(new SortOrder(field, Order.ASC));

        var queryParams = new SolrQueryByField(field, value);
        var queryParamsFID = new SolrQueryByField("franchiseId", franchiseId);

        List<ISolrQuery> query = new List<ISolrQuery>();
        query.Add(queryParams);
        query.Add(queryParamsFID);

        var queryCrit = new SolrMultipleCriteriaQuery(query, "AND");        

        if (themeFranchise != "Tous" && themeFranchise != null) 
        {
            query.Add(new SolrQueryByField("genre", themeFranchise));
        }        

        var results = await _solrConnection.QueryAsync(queryCrit, options);

        //var result = await _solrConnection.QueryAsync($"{field}:{value}&genre:test", cancellationToken);

        return results.ToList();

    }
}

