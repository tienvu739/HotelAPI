namespace HotelAPI.Models
{
    public class CHotel
    {
        public string IdHotel { get; set; } = null!;
        public string? NameHotel { get; set; }
        public string? AddressHotel { get; set; }
        public string? DescribeHotel { get; set; }
        public string? PolicyHotel { get; set; }
        public string? TypeHotel { get; set; }
        public bool? StatusHotel { get; set; }
        public string? IdHotelier { get; set; }
        public List<CImagehotel>? Images { get; set; }

        // Phương thức chuyển đổi từ Hotel sang CHotel
        public static CHotel chuyendoi(Hotel hotel)
        {
            return new CHotel
            {
                IdHotel = hotel.IdHotel,
                NameHotel = hotel.NameHotel,
                AddressHotel = hotel.AddressHotel,
                DescribeHotel = hotel.DescribeHotel,
                PolicyHotel = hotel.PolicyHotel,
                TypeHotel = hotel.TypeHotel,
                StatusHotel = hotel.StatusHotel,
                IdHotelier = hotel.IdHotelier,
                Images = hotel.Imagehotels.Select(i => new CImagehotel
                {
                    IdImageHotel = i.IdImageHotel,
                    ImageData = i.ImageData
                }).ToList()
            };
        }
    }
}
