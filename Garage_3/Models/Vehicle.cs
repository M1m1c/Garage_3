using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Regnummer")]
        public string RegNum { get; set; }

        [Display(Name = "Antal hjul")]
        public int? Wheels { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Fabrikat")]
        public string Brand { get; set; }

        [Display(Name = "Ankomsttid")]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Lämnar")]
        public DateTime DepartureTime { get; set; }
        public bool ParkedFlag { get; set; }



        //Foreign key
        [Display(Name = "Medlemsnummer")]
        public int MemberNumber { get; set; }
        
        [Display(Name = "Fordonstyp (num)")]
        public int TypeID { get; set; }
        
        public int ColorId { get; set; }

        //Navigation property
        public Owner Owner { get; set; }
        
        [Display(Name = "Fordonstyp")]
        public VehicleType VehicleType { get; set; }
        
        [Display(Name = "Färg")]
        public Color Color { get; set; }

    }
}
