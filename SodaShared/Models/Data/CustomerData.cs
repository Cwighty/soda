﻿using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("customer")]
public class CustomerData : BaseModel
{
    [PrimaryKey("id", true)]
    public string Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("phone")]
    public string Phone { get; set; }

    [Reference(typeof(ProductData), shouldFilterTopLevel:false)]
    public List<ProductData> Favorites { get; set; }
}

