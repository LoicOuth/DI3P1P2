namespace Application.PaymentIntent.Command.CreatePaymentIntentWithProductIdCommand;

public class CreatePaymentIntentWithProductIdCommandValidator : AbstractValidator<CreatePaymentIntentWithProductIdCommand>
{
    public CreatePaymentIntentWithProductIdCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Album).NotEmpty();
        RuleFor(x => x.Artist).NotEmpty();
        RuleFor(x => x.Cover).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DeezerId).NotEmpty();
    }
}
