
using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IKeycloackUsersHelper
{
    Task<List<UserModel>> GetAllUser(CancellationToken cancellationToken);
}
