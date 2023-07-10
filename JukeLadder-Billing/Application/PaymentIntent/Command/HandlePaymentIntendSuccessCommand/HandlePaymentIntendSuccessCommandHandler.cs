using MassTransit;

namespace Application.PaymentIntent.Command.HandlePaymentIntendSuccessCommand;

public class HandlePaymentIntendSuccessCommandHandler : IRequestHandler<HandlePaymentIntendSuccessCommand>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<HandlePaymentIntendSuccessCommandHandler> _logger;
    public HandlePaymentIntendSuccessCommandHandler(IPublishEndpoint publishEndpoint, ILogger<HandlePaymentIntendSuccessCommandHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Unit> Handle(HandlePaymentIntendSuccessCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Stripe.PaymentIntent paymentIntent = (Stripe.PaymentIntent)request.StripeEvent.Data.Object;
            if (
                paymentIntent.Metadata.TryGetValue("Amount", out string amount) &&
                paymentIntent.Metadata.TryGetValue("Currency", out string currency) &&
                paymentIntent.Metadata.TryGetValue("Description", out string description) &&
                paymentIntent.Metadata.TryGetValue("FranchiseId", out string franchiseId) &&
                paymentIntent.Metadata.TryGetValue("Title", out string title) &&
                paymentIntent.Metadata.TryGetValue("Artist", out string artist) &&
                paymentIntent.Metadata.TryGetValue("Album", out string album) &&
                paymentIntent.Metadata.TryGetValue("Cover", out string cover) &&
                paymentIntent.Metadata.TryGetValue("Duration", out string duration) &&
                paymentIntent.Metadata.TryGetValue("DeezerId", out string deezerId) &&
                !string.IsNullOrEmpty(amount) &&
                !string.IsNullOrEmpty(currency) &&
                !string.IsNullOrEmpty(description) &&
                !string.IsNullOrEmpty(franchiseId) &&
                !string.IsNullOrEmpty(title) &&
                !string.IsNullOrEmpty(artist) &&
                !string.IsNullOrEmpty(album) &&
                !string.IsNullOrEmpty(cover) &&
                !string.IsNullOrEmpty(duration) &&
                !string.IsNullOrEmpty(deezerId) &&
                !string.IsNullOrEmpty(paymentIntent.Id)
                )
            {
                var message = new PaymentIntendSucessfull()
                {
                    Id = paymentIntent.Id,
                    FranchiseId = franchiseId,
                    Title = title,
                    Artist = artist,
                    Album = album,
                    Cover = cover,
                    DeezerId = deezerId,
                    PaymentIntendId = paymentIntent.Id,
                    Duration = int.Parse(duration)
                };
                await _publishEndpoint.
                    Publish(message, cancellationToken);
            }
            else
            {
                throw new BillingException("FranchiseId or TrackId not found in metadata");
            }

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during successful payment intent with error {error}", ex.Message);
            throw;
        }
    }
}
