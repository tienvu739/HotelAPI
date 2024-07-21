namespace HotelAPI.Models
{
    public class CRoom
    {
        public string IdRoom { get; set; } = null!;
        public string? NameRoom { get; set; }
        public double? AreaRoom { get; set; }
        public int? People { get; set; }
        public string? PolicyRoom { get; set; }
        public int? BedNumber { get; set; }
        public bool? StatusRoom { get; set; }
        public string? TypeRoom { get; set; }
        public double? Price { get; set; }
        public string? IdHotel { get; set; }
        public List<CImageRoom> Imageroms { get; set;}
        public static CRoom chuyendoi(Room room)
        {
            return new CRoom
            {
                IdRoom = room.IdRoom,
                NameRoom = room.NameRoom,
                AreaRoom = room.AreaRoom,
                People = room.People,
                PolicyRoom = room.PolicyRoom,
                BedNumber = room.BedNumber,
                StatusRoom = room.StatusRoom,
                TypeRoom = room.TypeRoom,
                Price = room.Price,
                IdHotel = room.IdHotel,
                Imageroms = room.Imagerooms.Select(i => new CImageRoom
                {
                    IdImageRoom = i.IdImageRoom,
                    ImageData = i.ImageData,
                }).ToList()
            };
        }

    }
}
