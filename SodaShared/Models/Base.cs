namespace SodaShared.Models;

public class Base
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int BaseTypeId { get; set; }
    public BaseType BaseType { get; set; } = new BaseType();
}

public static class BaseExtensions
{
    public static Base Clone(this Base @base)
    {
        return new Base()
        {
            Id = @base.Id,
            Name = @base.Name,
            Description = @base.Description,
            Price = @base.Price,
            BaseTypeId = @base.BaseTypeId,
            BaseType = @base.BaseType
        };
    }
}