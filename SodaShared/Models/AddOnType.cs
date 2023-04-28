namespace SodaShared.Models;


public class AddOnType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is AddOnType other)
        {
            return Id == other.Id;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
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