namespace HMS.Models.Entity
{
    public class Room
    {
        public int ID { get; set; }
        public int RoomNumber { get; set; }
        public int HotelID { get; set; }

        public string Type { get; set; }
        public double PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        public Hotel Hotel { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
