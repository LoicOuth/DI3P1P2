using Application.Common.Models;

namespace Application.Users.Queries.GetAllUserQuery;
public record GetAllUserQuery() : IRequest<List<UserModel>>;