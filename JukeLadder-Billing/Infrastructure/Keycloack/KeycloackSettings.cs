using Application.Common.Constants;
using System.Data;

namespace Infrastructure.Keycloack;

public class KeycloackSettings
{
    public string TokenUrl { get; set; }
    public string BaseUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

    public KeycloackSettings()
    {
        TokenUrl= Environment.GetEnvironmentVariable(EnvConst.KEYCLOAK_TOKEN_URL) ?? throw new InvalidConstraintException($"ENV {EnvConst.KEYCLOAK_TOKEN_URL} NOT SET");
        BaseUrl = Environment.GetEnvironmentVariable(EnvConst.KEYCLOAK_BASE_URL) ?? throw new InvalidConstraintException($"ENV {EnvConst.KEYCLOAK_BASE_URL} NOT SET");
        ClientId = Environment.GetEnvironmentVariable(EnvConst.KEYCLOAK_CLIENT_ID) ?? throw new InvalidConstraintException($"ENV {EnvConst.KEYCLOAK_CLIENT_ID} NOT SET");
        ClientSecret = Environment.GetEnvironmentVariable(EnvConst.KEYCLOAK_CLIENT_SECRET) ?? throw new InvalidConstraintException($"ENV {EnvConst.KEYCLOAK_CLIENT_SECRET} NOT SET");
        
    }

}
