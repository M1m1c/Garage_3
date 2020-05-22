using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class EditVehicleViewModel
    {
        [Required]
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
        //[Remote(action: "DoesColorTypeExist", controller: "Remote", HttpMethod = "POST", ErrorMessage = "Finns ingen färg vid det namnet")]
        [Display(Name = "Färg")]
        public string ColorName { get; set; }

        [Required(ErrorMessage = "Ange fordonstyp")]
        //[Remote(action: "DoesVehicleTypeExist", controller: "Remote", HttpMethod = "POST", ErrorMessage = "Finns ingen fordonstyp vid det namnet")]
        [Display(Name = "Fordonstyp")]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Ange ägare")]
        [Remote(action: "DoesOwnerExists", controller: "Remote", HttpMethod = "POST", ErrorMessage = "Finns ingen användare med det användarnamnet")]
        [Display(Name = "Ägare")]
        public string Owner { get; set; }

        
        
    }
}

