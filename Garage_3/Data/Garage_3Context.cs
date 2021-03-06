﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_3.Models;

namespace Garage_3.Data
{
    public class Garage_3Context : DbContext
    {
        public Garage_3Context(DbContextOptions<Garage_3Context> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }
        
        public DbSet<Color> Colors { get; set; }
    }
}
