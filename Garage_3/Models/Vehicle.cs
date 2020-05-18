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
        public string RegNum { get; set; }

        public int? Wheels { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public DateTime ArrivalTime { get; set; }

      

        //Foreign key
        public int MemberNumber { get; set; }
        public int TypeID { get; set; }
        public int ColorId { get; set; }

        //Navigation property
        public Owner Owner { get; set; }
        public VehicleType VehicleType { get; set; }
        public Color Color { get; set; }

    }
}
