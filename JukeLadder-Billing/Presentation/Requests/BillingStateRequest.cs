namespace Presentation.Requests;

public record UpdateBillingStateRequest(string FranchiseId, bool Enable);