namespace HMS.ViewModel
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double TotalPrice { get; set; }

        public List<Room> AvailableRoom { get; set; }
        public List<Customer> Customer { get; set; }
    }
}
