using Newtonsoft.Json;

namespace SodaShared.Models;

public class BaseProduct
{
    [JsonProperty("base")]
    public string Base { get; set; }
    [JsonProperty("products")]
    public List<Product> Products { get; set; }

    public BaseProduct()
    {
    }
    public BaseProduct(string name, List<Product> products)
    {
        Base = name;
        Products = products;
    }
}
