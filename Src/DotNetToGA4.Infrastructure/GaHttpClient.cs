using DotNetToGA4.Infrastructure.Models;
using System.Text;
using System.Text.Json;

namespace DotNetToGA4.Infrastructure;

public sealed class GaHttpClient : IGaHttpClient
{
    private readonly HttpClient httpClient;
    private readonly string clientId;
    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly Uri apiEndpoint;
    /// <summary>
    /// /// To test your data use this one
    /// https://developers.google.com/analytics/devguides/collection/protocol/ga4/validating-events?client_type=firebase
    /// </summary>
    private readonly Uri apiEndpointDebug;

    public GaHttpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        clientId = "";

        jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };

        string apiUrl = "https://www.google-analytics.com/mp/collect";
        string measurementId = "G-XXXXXXXXXX";
        string apiSecret = "<secret_value>";
        var uriBuilder = new UriBuilder(apiUrl);
        uriBuilder.Query = $"measurement_id={measurementId}&api_secret={apiSecret}";

        apiEndpoint = uriBuilder.Uri;

        string apiUrlDebug = "https://www.google-analytics.com/debug/mp/collect";
        var uriBuildedebug = new UriBuilder(apiUrlDebug);
        uriBuildedebug.Query = $"measurement_id={measurementId}&api_secret={apiSecret}";

        apiEndpointDebug = uriBuildedebug.Uri;
    }

    public async Task<Result> PostGaEvents(IEnumerable<Event> events, bool testEvents = false)
    {
        var dataToSend = new GaRoot()
        {
            ClientId = clientId,
            events = events
        };

        var json = JsonSerializer.Serialize(dataToSend, jsonSerializerOptions);
        using (var httpContent = new StringContent(json, Encoding.UTF8, "application/json"))
        {
            var endpointToUse = apiEndpoint;
            if (testEvents)
            {
                endpointToUse = apiEndpointDebug;
            }
            var r = await httpClient.PostAsync(endpointToUse, httpContent);
            var msg = await r.Content.ReadAsStringAsync();
            return new Result(r.IsSuccessStatusCode, msg);
        }
    }
}


