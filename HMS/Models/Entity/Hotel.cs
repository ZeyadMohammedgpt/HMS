using System.ComponentModel.DataAnnotations;

namespace HMS.Models.Entity
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
