using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models
{
    public class Owner
    {
        [Key]
        [Display(Name = "Medlemsnummer")]
        public int MemberNumber { get; set; }
        
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        //Navigation Property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
