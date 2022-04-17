using Microsoft.EntityFrameworkCore;
using ReservationsApi.Models;

namespace ReservationsApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SHRIKANT-PC\MSSQLSERVER01;Initial Catalog=ReservationApiDb;Integrated Security=True");
        }
    }
}
