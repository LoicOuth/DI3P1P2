namespace Application.Franchises.Command.CreateFranchiseCommand;

public class CreateFranchiseCommandValidator : AbstractValidator<CreateFranchiseCommand>
{
    public CreateFranchiseCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
