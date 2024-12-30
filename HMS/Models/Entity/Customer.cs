namespace HMS.Models.Entity
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
