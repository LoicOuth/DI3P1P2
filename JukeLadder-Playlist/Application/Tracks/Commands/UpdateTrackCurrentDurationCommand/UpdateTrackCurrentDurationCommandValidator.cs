namespace Application.Tracks.Commands.UpdateTrackCurrentDurationCommand;

public class UpdateTrackCurrentDurationCommandValidator : AbstractValidator<UpdateTrackCurrentDurationCommand>
{
	public UpdateTrackCurrentDurationCommandValidator()
	{
		RuleFor(x => x.TrackId).NotEmpty();
		RuleFor(x => x.NewDuration).NotEmpty();	
	}
}
