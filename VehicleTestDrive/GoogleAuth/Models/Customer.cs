namespace GoogleAuth.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
