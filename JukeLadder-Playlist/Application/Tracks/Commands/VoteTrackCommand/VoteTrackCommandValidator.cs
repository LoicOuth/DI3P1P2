namespace Application.Tracks.Commands.VoteTrackCommand;

public class VoteTrackCommandValidator : AbstractValidator<VoteTrackCommand>
{
	public VoteTrackCommandValidator()
	{
        RuleFor(x => x.TrackId).NotEmpty();
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.Action)
            .NotEmpty()
            .Must(x => x.Equals("up") || x.Equals("down"))
            .WithMessage("Action must be 'up' or 'down'");
    }
}
