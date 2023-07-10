using Application.Common.Constants;
using System.Data;
using System.Text;

namespace Infrastructure.Solr;

public class SolrSettings
{
    public string Url { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public SolrSettings()
    {
        Url = Environment.GetEnvironmentVariable(EnvConst.SOLR_URL) ?? throw new InvalidConstraintException($"ENV {EnvConst.SOLR_URL} NOT SET");
        Username = Environment.GetEnvironmentVariable(EnvConst.SOLR_USERNAME) ?? throw new InvalidConstraintException($"ENV {EnvConst.SOLR_USERNAME} NOT SET");
        Password = Environment.GetEnvironmentVariable(EnvConst.SOLR_PASSWORD) ?? throw new InvalidConstraintException($"ENV {EnvConst.SOLR_PASSWORD} NOT SET");
    }

    public string GetCredentialBase64()
    {
        var byteArray = Encoding.ASCII.GetBytes($"{Username}:{Password}");

        return  Convert.ToBase64String(byteArray);
    }
}