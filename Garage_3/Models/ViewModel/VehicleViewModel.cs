using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class VehicleViewModel
    {

        public string RegNum { get; set; }

        public string VColor { get; set; }

        public string VType { get; set; }

        public string OwnerUserName { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
