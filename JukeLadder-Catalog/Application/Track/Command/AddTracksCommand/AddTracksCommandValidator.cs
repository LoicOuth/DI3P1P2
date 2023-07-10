namespace Application.Track.Command.AddTracksCommand;
public class AddTracksCommandValidator : AbstractValidator<AddTracksCommand>
{
    public AddTracksCommandValidator()
    {
        RuleFor(x => x.IdPlaylist).NotEmpty();
        RuleFor(x => x.IdFranchise).NotEmpty();
    }
}

