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
        [Display(Name = "Regnummer")]
        public string RegNum { get; set; }
        
        [Required(ErrorMessage = "Ange antal hjul")]
        [Display(Name = "Antal hjul")]
        public int Wheels { get; set; }
        
        [Required(ErrorMessage = "Ange modell")]
        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Ange fabrikat")]
        [Display(Name = "Fabrikat")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Ange färg")]
        [Display(Name = "Färg")]
        public string ColorName { get; set; }

        [Required(ErrorMessage = "Ange fordonstyp")]
        [Display(Name = "Fordonstyp")]
        public string VehicleType { get; set; }
    }
}
