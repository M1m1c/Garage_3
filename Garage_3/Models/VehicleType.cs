﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        public string VehicleTypeName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}