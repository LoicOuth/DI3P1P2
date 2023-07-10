namespace Application.PlaylistStates.Command.UpdatePlaylistStateCommand;

public class UpdatePlaylistStateCommandValidator : AbstractValidator<UpdatePlaylistStateCommand>
{
    public UpdatePlaylistStateCommandValidator()
    {
        RuleFor(x => x.FranchiseId).NotNull().NotEmpty();
        RuleFor(x => x.Enable).NotNull();
    }
}
