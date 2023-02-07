using DotNetToGA4.Infrastructure.Models;
using System.Text;
using System.Text.Json;

namespace DotNetToGA4.Infrastructure;

public sealed class GaHttpClient
{
    private readonly HttpClient httpClient;
    private readonly string clientId;
    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly Uri apiEndpoint;

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
    }

    public async Task<Result> PostGaEvents(IEnumerable<Event> events)
    {
        var dataToSend = new GaRoot()
        {
            ClientId = clientId,
            events = events
        };

        var json = JsonSerializer.Serialize(dataToSend, jsonSerializerOptions);
        using (var httpContent = new StringContent(json, Encoding.UTF8, "application/json"))
        {
            var r = await httpClient.PostAsync(apiEndpoint, httpContent);
            var msg = await r.Content.ReadAsStringAsync();
            return new Result(r.IsSuccessStatusCode, msg);
        }
    }

}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);





