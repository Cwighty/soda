using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models
{
    public class ProductSize
    {
        public string Size { get; set; }
        public string Img { get; set; }

        public ProductSize(string size, string img)
        {
            Size = size;
            Img = img;
        }
    }
}
