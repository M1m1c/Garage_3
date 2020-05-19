using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class OwnerViewModel
    {
        public int MemberNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }


        //Navigation Property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
