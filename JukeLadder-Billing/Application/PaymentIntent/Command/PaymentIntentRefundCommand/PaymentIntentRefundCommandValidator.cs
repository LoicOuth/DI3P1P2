namespace Application.PaymentIntent.Command.PaymentIntentRefundCommand;

public class PaymentIntentRefundCommandValidator : AbstractValidator<PaymentIntentRefundCommand>
{
	public PaymentIntentRefundCommandValidator()
	{
        RuleFor(x => x.PaymentIntendId).NotNull().NotEmpty();
    }
}
