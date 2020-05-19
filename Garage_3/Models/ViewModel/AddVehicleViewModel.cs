using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class AddVehicleViewModel
    {
        [Required]
        [Remote(action: "RegNumExists", controller: "Vehicles", HttpMethod = "POST", ErrorMessage = "Registreringsnummer finns redan")]
        public string RegNum { get; set; }
        
        [Required]
        public int Wheels { get; set; }
        
        [Required]
        public string Model { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string ColorName { get; set; }

        [Required]
        public string VehicleType { get; set; }
    }
}
