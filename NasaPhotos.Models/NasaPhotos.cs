using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NasaPhotos.Models
{
    public class NasaMarsPhotos
    {
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
