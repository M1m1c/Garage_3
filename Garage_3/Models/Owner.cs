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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Foreign Key
        public ICollection<string> VehicleRegNums { get; set; }
    }
}
