namespace Application.Track.Dto;

public class SuggestionSolrDto
{
    public string Term { get; set; }
    public int Weight { get; set; }
    public string Type { get; set; }
    public SuggestionSolrDto(string term, int weight, string type)
    {
        Term = term;
        Weight = weight;
        Type = type;
    }

}
