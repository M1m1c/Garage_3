using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_3.Models;

namespace Garage_3.Data
{
    public class Garage_3Context : DbContext
    {
        public Garage_3Context (DbContextOptions<Garage_3Context> options)
            : base(options)
        {
        }

        public DbSet<Garage_3.Models.Vehicle> Vehicle { get; set; }

        public DbSet<Garage_3.Models.Owner> Owners { get; set; }

        public DbSet<Garage_3.Models.VehicleType> VehicleTypes { get; set; }
        
        public DbSet<Garage_3.Models.Color> Colors { get; set; }
    }
}
