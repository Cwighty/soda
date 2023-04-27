namespace SodaShared.Models;

public class BaseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Size> Sizes { get; set; } = new();
    public override bool Equals(object? obj)
    {
        if (obj is BaseType other)
        {
            return Id == other.Id;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return Id;
    }
}

public static class BaseTypeExtensions
{
    public static BaseType Clone(this BaseType baseType)
    {
        return new BaseType()
        {
            Id = baseType.Id,
            Name = baseType.Name,
            Sizes = baseType.Sizes
        };
    }
}