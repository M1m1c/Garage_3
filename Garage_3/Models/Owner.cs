using Microsoft.AspNetCore.Mvc;
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
        public int MemberNumber { get; set; }

        [Remote(action: "UserNameExists",controller:"Vehicles",HttpMethod ="POST",ErrorMessage ="Användar Namnet finns redan")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [Remote(action: "EmailExists", controller: "Vehicles", HttpMethod = "POST", ErrorMessage = "Emailadressen används redan")]
        public string Email { get; set; }

        [Remote(action: "PhoneExists", controller: "Vehicles", HttpMethod = "POST", ErrorMessage = "Telefonnumret används redan")]
        public string Telephone { get; set; }

        //Navigation Property
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
