using Microsoft.EntityFrameworkCore;
using CustomersApi.Models;

namespace CustomersApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CustomerApiDb;");
            optionsBuilder.UseSqlServer(@"Data Source=SHRIKANT-PC\MSSQLSERVER01;Initial Catalog=CustomerApiDb;Integrated Security=True");
        }
    }
}
