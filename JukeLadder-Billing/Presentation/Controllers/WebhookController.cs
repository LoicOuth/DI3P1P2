using Application.Common.Interfaces;
using Application.PaymentIntent.Command.HandlePaymentIntendSuccessCommand;
using Newtonsoft.Json;
using Stripe;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly string _endPointSecret;
    public WebhookController(IMediator mediator, IStripeSettings stripeSettings)
    {
        _mediator = mediator;
        _endPointSecret = stripeSettings.WebhookSecret;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    "whsec_273523e210eed9dd3d41f055e4beced2ddd46441b42317928df5b91095029b1c",
                    throwOnApiVersionMismatch: false);

            var data = JsonConvert.DeserializeObject<Rootobject>(json);

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                await _mediator.Send(new HandlePaymentIntendSuccessCommand(stripeEvent));
            }

            return Ok();
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
            throw;
        }
    }
}


public class Rootobject
{
    public string id { get; set; }
    public string _object { get; set; }
    public string api_version { get; set; }
    public int created { get; set; }
    public Data data { get; set; }
    public bool livemode { get; set; }
    public int pending_webhooks { get; set; }
    public Request request { get; set; }
    public string type { get; set; }
}

public class Data
{
    public Object[] data { get; set; }
}

public class Object
{
    public string id { get; set; }
    public string _object { get; set; }
    public int amount { get; set; }
    public int amount_capturable { get; set; }
    public Amount_Details amount_details { get; set; }
    public int amount_received { get; set; }
    public object application { get; set; }
    public object application_fee_amount { get; set; }
    public Automatic_Payment_Methods automatic_payment_methods { get; set; }
    public object canceled_at { get; set; }
    public object cancellation_reason { get; set; }
    public string capture_method { get; set; }
    public Charges charges { get; set; }
    public string client_secret { get; set; }
    public string confirmation_method { get; set; }
    public int created { get; set; }
    public string currency { get; set; }
    public object customer { get; set; }
    public string description { get; set; }
    public object invoice { get; set; }
    public object last_payment_error { get; set; }
    public bool livemode { get; set; }
    public Metadata1 metadata { get; set; }
    public object next_action { get; set; }
    public object on_behalf_of { get; set; }
    public string payment_method { get; set; }
    public Payment_Method_Options payment_method_options { get; set; }
    public string[] payment_method_types { get; set; }
    public object processing { get; set; }
    public object receipt_email { get; set; }
    public object review { get; set; }
    public object setup_future_usage { get; set; }
    public object shipping { get; set; }
    public object source { get; set; }
    public object statement_descriptor { get; set; }
    public object statement_descriptor_suffix { get; set; }
    public string status { get; set; }
    public object transfer_data { get; set; }
    public object transfer_group { get; set; }
}

public class Amount_Details
{
    public Tip tip { get; set; }
}

public class Tip
{
}

public class Automatic_Payment_Methods
{
    public bool enabled { get; set; }
}

public class Charges
{
    public string _object { get; set; }
    public Datum[] data { get; set; }
    public bool has_more { get; set; }
    public int total_count { get; set; }
    public string url { get; set; }
}

public class Datum
{
    public string id { get; set; }
    public string _object { get; set; }
    public int amount { get; set; }
    public int amount_captured { get; set; }
    public int amount_refunded { get; set; }
    public object application { get; set; }
    public object application_fee { get; set; }
    public object application_fee_amount { get; set; }
    public string balance_transaction { get; set; }
    public Billing_Details billing_details { get; set; }
    public string calculated_statement_descriptor { get; set; }
    public bool captured { get; set; }
    public int created { get; set; }
    public string currency { get; set; }
    public object customer { get; set; }
    public string description { get; set; }
    public object destination { get; set; }
    public object dispute { get; set; }
    public bool disputed { get; set; }
    public object failure_balance_transaction { get; set; }
    public object failure_code { get; set; }
    public object failure_message { get; set; }
    public Fraud_Details fraud_details { get; set; }
    public object invoice { get; set; }
    public bool livemode { get; set; }
    public Metadata metadata { get; set; }
    public object on_behalf_of { get; set; }
    public object order { get; set; }
    public Outcome outcome { get; set; }
    public bool paid { get; set; }
    public string payment_intent { get; set; }
    public string payment_method { get; set; }
    public Payment_Method_Details payment_method_details { get; set; }
    public object receipt_email { get; set; }
    public object receipt_number { get; set; }
    public string receipt_url { get; set; }
    public bool refunded { get; set; }
    public Refunds refunds { get; set; }
    public object review { get; set; }
    public object shipping { get; set; }
    public object source { get; set; }
    public object source_transfer { get; set; }
    public object statement_descriptor { get; set; }
    public object statement_descriptor_suffix { get; set; }
    public string status { get; set; }
    public object transfer_data { get; set; }
    public object transfer_group { get; set; }
}

public class Billing_Details
{
    public Address address { get; set; }
    public object email { get; set; }
    public object name { get; set; }
    public object phone { get; set; }
}

public class Address
{
    public object city { get; set; }
    public object country { get; set; }
    public object line1 { get; set; }
    public object line2 { get; set; }
    public object postal_code { get; set; }
    public object state { get; set; }
}

public class Fraud_Details
{
}

public class Metadata
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Cover { get; set; }
    public string FranchiseId { get; set; }
    public string Currency { get; set; }
    public string DeezerId { get; set; }
    public string Artist { get; set; }
    public string Duration { get; set; }
    public string Album { get; set; }
    public string Amount { get; set; }
}

public class Outcome
{
    public string network_status { get; set; }
    public object reason { get; set; }
    public string risk_level { get; set; }
    public int risk_score { get; set; }
    public string seller_message { get; set; }
    public string type { get; set; }
}

public class Payment_Method_Details
{
    public Card card { get; set; }
    public string type { get; set; }
}

public class Card
{
    public string brand { get; set; }
    public Checks checks { get; set; }
    public string country { get; set; }
    public int exp_month { get; set; }
    public int exp_year { get; set; }
    public string fingerprint { get; set; }
    public string funding { get; set; }
    public object installments { get; set; }
    public string last4 { get; set; }
    public object mandate { get; set; }
    public string network { get; set; }
    public object three_d_secure { get; set; }
    public object wallet { get; set; }
}

public class Checks
{
    public object address_line1_check { get; set; }
    public object address_postal_code_check { get; set; }
    public string cvc_check { get; set; }
}

public class Refunds
{
    public string _object { get; set; }
    public object[] data { get; set; }
    public bool has_more { get; set; }
    public int total_count { get; set; }
    public string url { get; set; }
}

public class Metadata1
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Cover { get; set; }
    public string FranchiseId { get; set; }
    public string Currency { get; set; }
    public string DeezerId { get; set; }
    public string Artist { get; set; }
    public string Duration { get; set; }
    public string Album { get; set; }
    public string Amount { get; set; }
}

public class Payment_Method_Options
{
    public Bancontact bancontact { get; set; }
    public Card1 card { get; set; }
    public Eps eps { get; set; }
    public Giropay giropay { get; set; }
    public Ideal ideal { get; set; }
    public Link link { get; set; }
}

public class Bancontact
{
    public string preferred_language { get; set; }
}

public class Card1
{
    public object installments { get; set; }
    public object mandate_options { get; set; }
    public object network { get; set; }
    public string request_three_d_secure { get; set; }
}

public class Eps
{
}

public class Giropay
{
}

public class Ideal
{
}

public class Link
{
    public object persistent_token { get; set; }
}

public class Request
{
    public string id { get; set; }
    public string idempotency_key { get; set; }
}
