using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models
{
    public class Color
    {
        public int Id { get; set; }
        [Display(Name = "Färg")]

        [Required(ErrorMessage ="Ange färg")]
        [Remote(action: "ColorNameSameOrUnique", controller: "Remote", HttpMethod = "POST", AdditionalFields = nameof(Id), ErrorMessage = "Färgen finns redan")]
        public string ColorName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
