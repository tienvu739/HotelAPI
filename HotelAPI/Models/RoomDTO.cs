namespace HotelAPI.Models
{
    public class RoomDTO
    {
        public string? NameRoom { get; set; }
        public double? AreaRoom { get; set; }
        public int? People { get; set; }
        public string? PolicyRoom { get; set; }
        public int? BedNumber { get; set; }
        public bool? StatusRoom { get; set; }
        public string? TypeRoom { get; set; }
        public double? Price { get; set; }
        public string? IdHotel { get; set; }
        public List<ImageDtoRoom> Images { get; set; } = new List<ImageDtoRoom>();
    }
    public class ImageDtoRoom
    {
        public string ImageData { get; set; } = string.Empty;
    }
}
