using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

[Table("addon")]
public class AddOnData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Reference(typeof(AddOnTypeData))]
    public AddOnTypeData AddOnType { get; set; }
}

public class AddOn
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public AddOnType AddOnType { get; set; }
}

public static class AddOnExtensions
{
    public static AddOn ToAddOn(this AddOnData addOn)
    {
        return new AddOn
        {
            Id = addOn.Id,
            Name = addOn.Name,
            Price = addOn.Price,
            AddOnType = addOn.AddOnType.ToAddOn()
        };
    }

    public static List<AddOn> ToAddOns(this List<AddOnData> datas)
    {
        return datas.Select(a => a.ToAddOn()).ToList();
    }
}


