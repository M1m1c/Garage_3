using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class ColorViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Färg")]
        public string ColorName { get; set; }

        public bool IsUsed { get; set; }
    }
}
