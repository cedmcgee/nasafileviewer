using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NasaPhotos.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public int Sol { get; set; }
        public Camera camera { get; set; }
        public string Img_Src { get; set; }
        public DateTime Earth_Date { get; set; }
        public Rover rover { get; set; }

    }
}
