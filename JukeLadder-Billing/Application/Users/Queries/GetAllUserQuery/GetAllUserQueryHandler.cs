using Application.Common.Models;

namespace Application.Users.Queries.GetAllUserQuery;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserModel>>
{
    private readonly ILogger<GetAllUserQueryHandler> _logger;
    private readonly IKeycloackUsersHelper _usersHelper;

    public GetAllUserQueryHandler(ILogger<GetAllUserQueryHandler> logger, IKeycloackUsersHelper usersHelper)
    {
        _logger = logger;
        _usersHelper = usersHelper;
    }

    public async Task<List<UserModel>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get all users");
            return await _usersHelper.GetAllUser(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during getting all users with {error}", ex.Message);
            throw;
        }
    }
}
