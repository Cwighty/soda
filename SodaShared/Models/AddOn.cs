namespace SodaShared.Models;

public class AddOn
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public AddOnType AddOnType { get; set; }
}

public static class AddOnExtensions
{
    public static AddOn Clone(this AddOn addOn)
    {
        return new AddOn()
        {
            Id = addOn.Id,
            Name = addOn.Name,
            Price = addOn.Price,
            AddOnType = addOn.AddOnType
        };
    }
}