using Application.Common.Constants;
using System.Data;

namespace Infrastructure.Deezer;

public class DeezerSettings : IDeezerSettings
{
    public string DeezerUrl { get; set; }

    public DeezerSettings()
    {
        DeezerUrl = Environment.GetEnvironmentVariable(EnvConst.DEEZER_URL) ?? throw new InvalidConstraintException($"ENV {EnvConst.DEEZER_URL} NOT SET");
    }
}
