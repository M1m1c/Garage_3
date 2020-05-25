using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Display(Name = "Fordonstyp")]
        [Required(ErrorMessage = "Ange fordonstyp")]
        [Remote(action: "VehicleTypeSameOrUnique", controller: "Remote", HttpMethod = "POST", AdditionalFields = nameof(Id), ErrorMessage = "Fordonstypen finns redan")]
        public string VehicleTypeName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
