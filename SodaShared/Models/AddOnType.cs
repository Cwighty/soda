namespace SodaShared.Models;


public class AddOnType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class AddOnTypeExtensions
{
    public static AddOnType Clone(this AddOnType addOnType)
    {
        return new AddOnType()
        {
            Id = addOnType.Id,
            Name = addOnType.Name
        };
    }
}