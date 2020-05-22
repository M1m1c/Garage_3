using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_3.Models.ViewModel
{
    public class EditOwnerViewModel
    {
        [Display(Name = "Medlemsnummer")]
        public int MemberNumber { get; set; }

        [Remote(action: "UserNameSameOrUnique", controller: "Vehicles", HttpMethod = "POST", AdditionalFields = nameof(MemberNumber), ErrorMessage = "Användar Namnet finns redan")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Remote(action: "EmailSameOrUnique", controller: "Vehicles", HttpMethod = "POST", AdditionalFields = nameof(MemberNumber), ErrorMessage = "Emailadressen används redan")]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Remote(action: "PhoneSameOrUnique", controller: "Vehicles", HttpMethod = "POST", AdditionalFields = nameof(MemberNumber), ErrorMessage = "Telefonnumret används redan")]
        [Display(Name = "Telefon")]
        public string Telephone { get; set; }
    }
}
