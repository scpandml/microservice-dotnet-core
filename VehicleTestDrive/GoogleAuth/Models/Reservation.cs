﻿namespace GoogleAuth.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public bool IsMailSent { get; set; }
    }
}
