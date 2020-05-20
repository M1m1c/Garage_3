using Garage_3.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage_3.Controllers
{
    public class ReceiptViewModel
    {
        [Display(Name = "Regnummer")]
        public string RegNum { get; set; }
        [Display(Name = "Typ")]
        public VehicleType VehicleType { get; set; }
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Display(Name = "Ankomst")]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Lämnar")]
        public DateTime DepartureTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:d' dygn'\\ hh' h '\\ mm' m'}")]
        [Display(Name = "Parkeringstid")]
        public TimeSpan TotalParkedTime { get; set; }

        [Display(Name = "Pris")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }
    }
}