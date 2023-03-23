using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models
{
    [Table("size")]
    public class SizeData : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public Decimal Price { get; set; }
        [Column("img")]
        public string Img { get; set; }
    }

    [ObservableObject]
    public partial class Size
    {
        public Size()
        {

        }
        public string Name { get; set; }
        public Decimal Price { get; set; }

        public string Img { get; set; }
        public string Path => IsSelected ? SelectedPath : UnSelectedPath;

        public string SelectedPath => $"white_{Img}";
        public string UnSelectedPath => $"black_{Img}";

        [NotifyPropertyChangedFor(nameof(Path))]
        [ObservableProperty]
        private bool isSelected;
    }
}