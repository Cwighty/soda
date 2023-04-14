namespace SodaShared.Models;

public class AddOn
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int AddOnTypeId { get; set; }
    public AddOnType AddOnType { get; set; } = new AddOnType();
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
            AddOnTypeId = addOn.AddOnTypeId,
            AddOnType = addOn.AddOnType
        };
    }
}