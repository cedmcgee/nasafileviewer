using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NasaPhotos.Models
{
    public class Rover
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Landing_Date { get; set; }
        public DateTime Launch_Date { get; set; }
        public string Status { get; set; }
        public int Max_Sol { get; set; }
        public DateTime Max_Date { get; set; }
        public int Total_Photos { get; set; }
        public ICollection<RoverCamera> Cameras {get;set;} = new List<RoverCamera>();
    }
}
