
namespace Application.Common.Interfaces;

public interface IStripeSettings
{
    string ProductPromote { get; }
    string ProductDemote { get; }
    string SecretKey { get; }
    string WebhookSecret { get; }
}
