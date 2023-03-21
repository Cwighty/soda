﻿using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

[Table("base")]
public class BaseData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("type_id")]
    public int TypeId { get; set; }
    [Reference(typeof(BaseTypeData))]
    public BaseTypeData BaseType { get; set; }
}

public class Base
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int TypeId { get; set; }
    public BaseType BaseType { get; set; }
}

public static class BaseExtensions
{
    public static Base ToBase(this BaseData data)
    {
        return new Base
        {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            Price = data.Price,
            TypeId = data.TypeId,
            BaseType = data.BaseType.ToBaseType()
        };
    }
}
