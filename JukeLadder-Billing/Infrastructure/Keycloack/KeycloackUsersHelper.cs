
using Application.Common.Models;
using System.Net.Http.Json;

namespace Infrastructure.Keycloack;

public class KeycloackUsersHelper : IKeycloackUsersHelper
{
    private readonly KeycloackSettings _settings;
    private readonly KeycloackAuthenticationHelper _authHelper;
    private readonly HttpClient http = new();

    public KeycloackUsersHelper(KeycloackAuthenticationHelper authHelper, KeycloackSettings settings)
    {
        _authHelper = authHelper;
        _settings = settings;
    }

    public async Task<List<UserModel>> GetAllUser(CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetAccessToken();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_settings.BaseUrl}/users");
        
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
        var responseMessage = await http.SendAsync(request, cancellationToken);
        var response = await responseMessage.Content.ReadFromJsonAsync(typeof(List<UserModel>), cancellationToken: cancellationToken);

        return response as List<UserModel>;

    }
}
