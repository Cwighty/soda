﻿using Postgrest.Attributes;
using Postgrest.Models;
using System.Collections.ObjectModel;

namespace SodaShared.Models.Data;

[Table("product")]
public class ProductData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("special_price")]
    public decimal? SpecialPrice { get; set; }
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    [Column("base_id")]
    public int BaseId { get; set; }
    [Reference(typeof(BaseData))]
    public BaseData Base { get; set; }
    [Reference(typeof(AddOnData), shouldFilterTopLevel:false)]
    public ObservableCollection<AddOnData> AddOns { get; set; }
}

