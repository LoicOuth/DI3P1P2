using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Presentation.Authentification;

public class ClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity!;

        if (claimsIdentity.IsAuthenticated)
        {
            var userRole = claimsIdentity.FindFirst((claim) => claim.Type == "realm_access");
            var content = JObject.Parse(userRole!.Value);
            foreach (var role in content["roles"]!)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }
        return Task.FromResult(principal);

    }
}
