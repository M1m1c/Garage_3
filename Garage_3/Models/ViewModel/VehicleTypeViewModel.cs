using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Fordonstyp")]    
        public string VehicleTypeName { get; set; }

        public bool IsUsed { get; set; }
    }
}
