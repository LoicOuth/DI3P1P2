namespace Application.Franchises.Command.DeleteFranchiseCommand;

public class DeleteFranchiseCommandValidator : AbstractValidator<DeleteFranchiseCommand>
{
    public DeleteFranchiseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty(); 
    }
}
