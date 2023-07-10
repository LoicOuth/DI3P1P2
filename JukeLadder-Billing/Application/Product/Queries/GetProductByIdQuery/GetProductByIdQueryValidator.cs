namespace Application.Product.Queries.GetProductByIdQuery;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}