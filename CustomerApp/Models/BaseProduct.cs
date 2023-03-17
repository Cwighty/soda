namespace CustomerApp.Models;

public class BaseProduct
{
    public string Base { get; set; }
    public List<ProductData> Products { get; set; }

    public BaseProduct(string name, List<ProductData> productDatas)
    {
        Base = name;
        Products = productDatas;
    }
}
