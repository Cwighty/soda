namespace CustomerApp.Models;

public class BaseProduct
{
    public string Base { get; set; }
    public List<Product> Products { get; set; }

    public BaseProduct(string name, List<Product> products)
    {
        Base = name;
        Products = products;
    }
}
