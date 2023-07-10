using Newtonsoft.Json.Linq;

namespace Infrastructure.Keycloack;

public class KeycloackAuthenticationHelper
{
	private readonly KeycloackSettings _settings;
	private readonly HttpClient http = new();
	public KeycloackAuthenticationHelper(KeycloackSettings keycloackSettings)
	{
		_settings= keycloackSettings;
	}

	public async Task<string> GetAccessToken()
	{
		var body =new Dictionary<string, string>()
		{
			{ "grant_type", "client_credentials" },
			{ "client_id", _settings.ClientId },
			{ "client_secret", _settings.ClientSecret }
        };

		var response = await http.PostAsync(_settings.TokenUrl, new FormUrlEncodedContent(body));

		var tokenResponse = await response.Content.ReadAsStringAsync();

		var x = JObject.Parse(tokenResponse);

		return x.GetValue("access_token")!.ToString();
	}
}
