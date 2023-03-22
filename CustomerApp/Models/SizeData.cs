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
        public string Name { get; set; }
        public Decimal Price { get; set; }
        
        public string Img { get; set; }
        public string Path => IsSelected ? SelectedPath : UnSelectedPath;

        public string SelectedPath { get; set; }
        public string UnSelectedPath { get; set; }

        [NotifyPropertyChangedFor(nameof(Path))]
        [ObservableProperty]
        private bool isSelected;

        public Size(string size, string img, Decimal price)
        {
            Img = img;
            SelectedPath = $"white_{img}";
            UnSelectedPath = $"black_{img}";
            Name = size;
            Price = price;
        }
    }

    public static class SizeExtensions
    {
        public static Size ToSize(this SizeData size)
        {
            return new Size(size.Name, size.Img, size.Price);
        }

        public static List<Size> ToSizes(this List<SizeData> sizes) {
            return sizes.Select(s => s.ToSize()).ToList();
        }
    }
}
