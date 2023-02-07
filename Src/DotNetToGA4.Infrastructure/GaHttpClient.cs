using DotNetToGA4.Infrastructure.Models;
using System.Text;
using System.Text.Json;

namespace DotNetToGA4.Infrastructure;

public sealed class GaHttpClient
{
    private readonly HttpClient httpClient;
    private readonly string clientId;
    private readonly string measurementId = "G-XXXXXXXXXX";
    private readonly string apiSecret = "<secret_value>";
    private readonly JsonSerializerOptions jsonSerializerOptions;

    public GaHttpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        clientId = "";
        jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
    }

    public async Task<Result> PostGaEvents(IEnumerable<Event> events)
    {
        var dataToSend = new GaRoot()
        {
            ClientId = clientId,
            events = events
        };
        var uriBuilder = new UriBuilder("https://www.google-analytics.com/mp/collect");
        uriBuilder.Query = $"measurement_id={measurementId}&api_secret={apiSecret}";
        var json = JsonSerializer.Serialize(dataToSend, jsonSerializerOptions);

        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var r = await httpClient.PostAsync(uriBuilder.Uri, httpContent);
        var msg = await r.Content.ReadAsStringAsync();
        return new Result(r.IsSuccessStatusCode, msg);
    }

}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);





