namespace Application.BillingStates.Queries.GetBillingStateQuery;

public record GetBillingStateQuery(string FranchiseId) : IRequest<bool>;
