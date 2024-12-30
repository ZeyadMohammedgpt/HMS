namespace HMS.Models.Entity
{
    public class Reservation
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double TotalPrice { get; set; }

        public Room Room { get; set; }
        public Customer Customer { get; set; }
    }
}
