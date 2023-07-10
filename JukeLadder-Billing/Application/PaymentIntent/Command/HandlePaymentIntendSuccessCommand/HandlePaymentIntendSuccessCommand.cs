namespace Application.PaymentIntent.Command.HandlePaymentIntendSuccessCommand;

public record HandlePaymentIntendSuccessCommand(Event StripeEvent) : IRequest;