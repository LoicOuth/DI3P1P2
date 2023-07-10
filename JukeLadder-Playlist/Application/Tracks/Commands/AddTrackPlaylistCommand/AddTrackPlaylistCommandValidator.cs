namespace Application.Tracks.Commands.AddTrackPlaylistCommand;

public class AddTrackPlaylistCommandValidator : AbstractValidator<AddTrackPlaylistCommand>
{
	public AddTrackPlaylistCommandValidator()
	{
		RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.DeezerId).NotEmpty();
		RuleFor(x => x.Artist).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Album).NotEmpty();
        RuleFor(x => x.Cover).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty(); 
    }
}
