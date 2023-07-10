namespace Application.Tracks.Commands.EndTrackCommand;

public class EndTrackCommandValidator : AbstractValidator<EndTrackCommand>
{
	public EndTrackCommandValidator()
	{
		RuleFor(x => x.TrackId).NotEmpty();
		RuleFor(x => x.FranchiseId).NotEmpty();
	}
}
