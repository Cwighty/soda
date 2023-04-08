using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerApp.Shared;

public class CheckoutInitiationResponse
{
    [JsonPropertyName("clientSecret")]
    public string ClientSecret { get; set; }

    [JsonPropertyName("orderNumber")]
    public int OrderNumber { get; set; }
}
