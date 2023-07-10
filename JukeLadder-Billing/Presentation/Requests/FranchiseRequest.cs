namespace Presentation.Requests;
public record UpdateFranchiseRequest(string UserId, string Name, string Theme);
public record GetFranchiseByIdQuery(string Id);
public record CreateFranchiseRequest(string UserId, string Name);
