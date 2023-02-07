using System.Text.Json.Serialization;

namespace DotNetToGA4.Infrastructure.Models;

public class GaRoot
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }
    public IEnumerable<Event> events { get; set; }
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);





