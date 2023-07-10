namespace Presentation.Requests;

public record CreatePaymentIntentRequest(string ProductId, string FranchiseId, string Title, string Artist, string Album, string Cover, int Duration, string Id);