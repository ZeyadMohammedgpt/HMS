namespace HMS.ViewModel;

public class RoomViewModel
{
    public int ID { get; set; }
    public int RoomNumber { get; set; }
    public int HotelID { get; set; }
    public string Type { get; set; } 
    public double PricePerNight { get; set; }
    public bool IsAvailable { get; set; }
    public List<Hotel> Hotels { get; set; }
    
}
