namespace Application.Franchises.Queries.GetFranchiseByIdQuery;

public class GetFranchiseByIdQueryValidator : AbstractValidator<GetFranchiseByIdQuery>
{
    public GetFranchiseByIdQueryValidator()
    {
        RuleFor(x => x.Query).NotEmpty();
    }
}
