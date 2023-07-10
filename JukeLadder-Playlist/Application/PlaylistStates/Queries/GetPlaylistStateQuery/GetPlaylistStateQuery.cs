namespace Application.PlaylistStates.Queries.GetPlaylistStateQuery;

public class GetPlaylistStateQuery : IRequest<bool>
{
    public string FranchiseId { get; set; }

	public GetPlaylistStateQuery(string franchiseId)
	{
		FranchiseId = franchiseId;
	}
}
