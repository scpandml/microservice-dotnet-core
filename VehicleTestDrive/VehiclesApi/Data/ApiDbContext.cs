﻿using Microsoft.EntityFrameworkCore;
using VehiclesApi.Models;

namespace VehiclesApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=VehicleApiDb;");
            optionsBuilder.UseSqlServer(@"Data Source=SHRIKANT-PC\MSSQLSERVER01;Initial Catalog=VehicleApiDb;Integrated Security=True");
        }
    }
}
