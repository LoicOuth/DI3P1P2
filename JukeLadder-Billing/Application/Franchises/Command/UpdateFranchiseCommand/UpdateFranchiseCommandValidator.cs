namespace Application.Franchises.Command.UpdateFranchiseCommand;

public class UpdateFranchiseCommandValidator : AbstractValidator<UpdateFranchiseCommand>
{
    public UpdateFranchiseCommandValidator()
    {
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Theme).NotEmpty();
    }
}
