using System;
using System.Collections.Generic;
using System.Text;

namespace NasaPhotos.Models
{
    public class ImageDates
    {
        public ICollection<ImageDate> ImageDate { get; set; } = new List<ImageDate>();
    }
}
