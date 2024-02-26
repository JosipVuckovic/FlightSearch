using FlightSearch.External.Amadeus.DTO;
using FlightSearch.Server.Models.Config;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace FlightSearch.External.Amadeus.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IAmadeusTokenApi _amadeusApi;
    private readonly AmadeusApiSettings _settings;

    private OAuthResponse? _oAuthResponse;
    private DateTimeOffset _expiresAt;

    public AuthHeaderHandler(IAmadeusTokenApi amadeusApi,
        IOptions<AmadeusApiSettings> options)
    {
        _amadeusApi = amadeusApi;
        _settings = options.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
        HttpRequestHeaders headers = request.Headers;
        AuthenticationHeaderValue authHeader = headers.Authorization;

        if (string.IsNullOrWhiteSpace(_oAuthResponse?.AccessToken) || DateTimeOffset.Now > _expiresAt)
        {
            try
            {
                //TODO: JV move to string constants
                _oAuthResponse = await _amadeusApi.GetAccessTokenAsync(new Dictionary<string, string>
                    {
                        {"grant_type", _settings.GrantType},
                        {"client_id", _settings.ApiKey},
                        {"client_secret", _settings.ApiSecret}
                    },
                    cancelToken);

                _expiresAt = DateTimeOffset.Now.AddSeconds(_oAuthResponse.ExpiresIn);
            }
            catch (Exception ex)
            {
                //TODO: logg ex
            }
        }

        if (authHeader != null)
            headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, _oAuthResponse.AccessToken);

        return await base.SendAsync(request, cancelToken);
    }
}