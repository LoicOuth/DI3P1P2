using Application.Tracks.Dtos;

namespace Application.Helpers;

public class TrackHelper
{
    public readonly IMongoDbHelper<Track> _trackHelper;
    public TrackHelper(IMongoDbHelper<Track> trackHelper)
    {
        _trackHelper = trackHelper;
    }
    public async Task<IEnumerable<TrackDto>> GetTrackWithFranchiseId(string franchiseId, CancellationToken cancellationToken)
    {
        var tracks = (await _trackHelper.GetAll(x => x.FranchiseId == franchiseId, cancellationToken))
            .Select(x =>
            new TrackDto
            {
                Id = x.Id,
                FranchiseId = x.FranchiseId,
                DeezerId = x.DeezerId,
                Title = x.Title,
                Artist = x.Artist,
                Album = x.Album,
                Cover = x.Cover,
                Duration = x.Duration,
                Upvotes = x.Upvotes,
                Downvotes = x.Downvotes,
                IsReading = x.IsReading,
                CurrentDuration = x.CurrentDuration,
                DatePromote = x.DatePromote,
                Position = x.Position
            }).OrderByDescending(x => x.IsReading)
            .ThenByDescending(x => x.DatePromote.HasValue)
            .ThenBy(x => x.DatePromote)
            .ThenByDescending(x => x.Upvotes.Count() - x.Downvotes.Count()).ToList();

        var result = tracks.Where(x=>x.Position == 0).ToList();
        var res = tracks.Where(track => track.Position != 0).OrderBy(x => x.Position).ToList();

        foreach (var t in res)
        {
                if (t.Position <= result.Count())
                {
                    result.Insert(t.Position, t);
                }
                else
                {
                    result.Add(t);

                }
        }

        return result;
    }

    public async Task<TrackDto?> GetTrackWithFranchiseIdAndTrackIdOrDefault(string franchiseId, string trackId, CancellationToken cancellationToken)
    {
        var track = await _trackHelper.GetAsync(x => x.FranchiseId == franchiseId && x.Id == trackId, cancellationToken);
        
        if (track == null) return null;
        
        return new TrackDto
        {
            Id = track.Id,
            FranchiseId = track.FranchiseId,
            Title = track.Title,
            Artist = track.Artist,
            Album = track.Album,
            Cover = track.Cover,
            Duration = track.Duration,
            Upvotes = track.Upvotes,
            Downvotes = track.Downvotes,
            IsReading = track.IsReading,
            CurrentDuration= track.CurrentDuration,
            DatePromote = track.DatePromote
        };
    }

    public async Task<Track?> GetNextTrack(string franchiseId, CancellationToken cancellationToken)
    {
        return (await _trackHelper.GetAll(x => x.FranchiseId == franchiseId, cancellationToken))
          .OrderByDescending(x => x.IsReading)
          .ThenByDescending(x => x.DatePromote.HasValue)
          .ThenBy(x => x.DatePromote)
          .ThenByDescending(x => x.Upvotes.Count() - x.Downvotes.Count()).First();
    }

    public async Task InsertAsync(string franchiseId, string deezerId, string title, string artist, string album, string cover, int duration, DateTime? dateTime, CancellationToken cancellationToken)
    {
        await _trackHelper.InsertAsync(new Track
        {
            FranchiseId = franchiseId,
            DeezerId = deezerId,
            Title = title,
            Artist = artist,
            Album = album,
            Cover = cover,
            Duration = duration,
            Upvotes = new List<string>(),
            Downvotes = new List<string>(),
            IsReading = false,
            CurrentDuration = 0,
            DatePromote = dateTime
        }, cancellationToken);
    }

    public async Task InsertFirstTracksAsync(string franchiseId, string deezerId, string title, string artist, string album, string cover, int duration, DateTime? dateTime, CancellationToken cancellationToken)
    {
        await _trackHelper.InsertAsync(new Track
        {
            FranchiseId = franchiseId,
            DeezerId = deezerId,
            Title = title,
            Artist = artist,
            Album = album,
            Cover = cover,
            Duration = duration,
            Upvotes = new List<string>(),
            Downvotes = new List<string>(),
            IsReading = true,
            CurrentDuration = 0,
            DatePromote = dateTime
        }, cancellationToken);
    }

    public async Task<bool> CheckIfExist(string franchiseId, string deezerId, CancellationToken cancellationToken)
    {
        return (await GetTrackWithFranchiseId(franchiseId, cancellationToken)).Any(x => x.DeezerId == deezerId);
    }

    public async Task<bool> CheckAnyTracks(string franchiseId, CancellationToken cancellationToken)
    {
        return (await GetTrackWithFranchiseId(franchiseId, cancellationToken)).Any();
    }
}
