using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models;

[Table("customer")]
public class CustomerData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
}
