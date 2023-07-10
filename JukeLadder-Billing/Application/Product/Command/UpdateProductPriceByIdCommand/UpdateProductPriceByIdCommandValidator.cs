namespace Application.Product.Command.UpdateProductPriceByIdCommand;

public class UpdateProductPriceByIdCommandValidator : AbstractValidator<UpdateProductPriceByIdCommand>
{
    public UpdateProductPriceByIdCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.NewPrice).NotEmpty();
        RuleFor(x => x.NewCurrency).NotEmpty();
    }
}
