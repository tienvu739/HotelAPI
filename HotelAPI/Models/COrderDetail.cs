namespace HotelAPI.Models
{
    public class COrderDetail
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public double? Price { get; set; }
        public string? IdUser { get; set; }
        public string? IdDiscount { get; set; }
        public string? IdRoom { get; set; }
        public string? HotelName { get; set; }
        public string? RoomName { get; set; }
    }
}
