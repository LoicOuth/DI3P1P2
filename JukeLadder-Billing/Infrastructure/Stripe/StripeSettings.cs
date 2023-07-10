using Application.Common.Constants;
using System.Data;

namespace Infrastructure.Stripe;

public class StripeSettings : IStripeSettings
{
    public string SecretKey { get; set; }
    public string PublishableKey { get; set; }
    public string WebhookSecret { get; set; }
    public string ProductPromote { get; set; }
    public string ProductDemote { get; set; }
    public StripeSettings()
    {
        SecretKey = Environment.GetEnvironmentVariable(EnvConst.STRIPE_SECRET_KEY) ?? throw new InvalidConstraintException($"ENV {EnvConst.STRIPE_SECRET_KEY} NOT SET");
        PublishableKey = Environment.GetEnvironmentVariable(EnvConst.STRIPE_PUBLISHABLE_KEY) ?? throw new InvalidConstraintException($"ENV {EnvConst.STRIPE_PUBLISHABLE_KEY} NOT SET");
        WebhookSecret = Environment.GetEnvironmentVariable(EnvConst.STRIPE_WEBHOOK_SECRET) ?? throw new InvalidConstraintException($"ENV {EnvConst.STRIPE_WEBHOOK_SECRET} NOT SET");
        ProductPromote = Environment.GetEnvironmentVariable(EnvConst.STRIPE_PRODUCT_PROMOTE_ID) ?? throw new InvalidConstraintException($"ENV {EnvConst.STRIPE_PRODUCT_PROMOTE_ID} NOT SET");
        ProductDemote = Environment.GetEnvironmentVariable(EnvConst.STRIPE_PRODUCT_DEMOTE_ID) ?? throw new InvalidConstraintException($"ENV {EnvConst.STRIPE_PRODUCT_DEMOTE_ID} NOT SET");

        StripeConfiguration.ApiKey = SecretKey;
    }
}
