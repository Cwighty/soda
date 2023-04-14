namespace SodaShared.Models;

public class BaseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Size> Sizes { get; set; }
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