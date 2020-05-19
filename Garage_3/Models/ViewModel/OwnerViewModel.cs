using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class OwnerViewModel
    {
        [Display(Name = "Medlemsnummer")]
        public int MemberNumber { get; set; }
        
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }


        //Navigation Property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
