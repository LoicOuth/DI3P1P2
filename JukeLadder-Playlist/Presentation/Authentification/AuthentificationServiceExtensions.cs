using Application.Common.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Cryptography;

namespace Presentation.Authentification;

public static class AuthentificationServiceExtensions
{
    private static RsaSecurityKey BuildRSAKey(string publicKeyJWT)
    {
        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(
            source: Convert.FromBase64String(publicKeyJWT),
            out _
        );
        var IssuerSigningKey = new RsaSecurityKey(rsa);
        return IssuerSigningKey;
    }

    public static void ConfigureJWT(this IServiceCollection services, bool IsDevelopment)
    {
        services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = Environment.GetEnvironmentVariable(EnvConst.KEYCLOAK_ISSUER) ?? throw new InvalidConstraintException($"ENV {EnvConst.KEYCLOAK_ISSUER} NOT SET"),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = BuildRSAKey(Environment.GetEnvironmentVariable(EnvConst.KEYCLOAK_RSA_PUBLIC_KEY) ?? throw new InvalidConstraintException($"ENV {EnvConst.KEYCLOAK_RSA_PUBLIC_KEY} NOT SET")),
                ValidateLifetime = true
            };

            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 401;
                    c.Response.ContentType = "text/plain";
                    if (IsDevelopment)
                    {
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                    return c.Response.WriteAsync("An error occured processing your authentication.");
                }
            };
        });
    }
}
