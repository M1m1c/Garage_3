using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
