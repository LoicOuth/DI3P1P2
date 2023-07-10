namespace Application.Tracks.Commands.UpdateTrackPositionCommand;
public class UpdateTrackPositionCommandValidator : AbstractValidator<UpdateTrackPositionCommand>
{
    public UpdateTrackPositionCommandValidator()
    {
        RuleFor(x => x.TrackId).NotEmpty();
        RuleFor(x => x.NewPosition).NotEmpty();
    }
}