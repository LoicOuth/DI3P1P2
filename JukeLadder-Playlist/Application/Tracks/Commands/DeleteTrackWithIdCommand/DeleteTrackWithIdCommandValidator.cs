namespace Application.Tracks.Commands.DeleteTrackWithIdCommand;

public class DeleteTrackWithIdCommandValidator : AbstractValidator<DeleteTrackWithIdCommand>
{
	public DeleteTrackWithIdCommandValidator()
	{
		RuleFor(x => x.TrackId).NotNull().NotEmpty();
	}
}
