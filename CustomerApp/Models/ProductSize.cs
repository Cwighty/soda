using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models
{
  [ObservableObject]
  public partial class ProductSize
  {
    public string Size { get; set; }
    public string Img => IsSelected ? SelectedPath : UnSelectedPath;

    public string SelectedPath { get; set; }
    public string UnSelectedPath { get; set; }

    [NotifyPropertyChangedFor(nameof(Img))]
    [ObservableProperty]
    private bool isSelected;
   

    public ProductSize(string size, string img)
    {
      SelectedPath = $"white_{img}";
      UnSelectedPath = $"black_{img}";
      Size = size;
    }
  }
}
