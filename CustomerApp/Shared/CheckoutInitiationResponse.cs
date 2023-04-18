using System.Text.Json.Serialization;

namespace CustomerApp.Shared;

public class CheckoutInitiationResponse
{
    [JsonPropertyName("clientSecret")]
    public string ClientSecret { get; set; }

    [JsonPropertyName("orderNumber")]
    public int OrderNumber { get; set; }
}
