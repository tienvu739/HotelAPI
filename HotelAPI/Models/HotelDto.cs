namespace HotelAPI.Models
{
    public class HotelDto
    {
        public string? NameHotel { get; set; }
        public string? AddressHotel { get; set; }
        public string? DescribeHotel { get; set; }
        public string? PolicyHotel { get; set; }
        public string? TypeHotel { get; set; }
        public bool? StatusHotel { get; set; }
        public string? IdHotelier { get; set; }
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
    }

    public class ImageDto
    {
        public string ImageData { get; set; } = string.Empty;
    }
}
