namespace HotelAPI.Models
{
    public class COrder
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public double? Price { get; set; }
        public string? IdUser { get; set; }
        public string? IdDiscount { get; set; }
        public string? IdRoom { get; set; } 
        public bool? Stastus { get; set; }

        public static COrder chuyendoi(Order x)
        {
            if (x == null) return null;
            return new COrder
            {
                DateCreated = x.DateCreated,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate,
                Price = x.Price,
                IdUser = x.IdUser,
                IdDiscount = x.IdDiscount,
                IdRoom = x.IdRoom,
                Stastus = x.Stastus,

            };
        }
        public static Order chuyendoi(COrder x)
        {
            if (x == null) return null;
            return new Order
            {
                DateCreated = x.DateCreated,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate,
                Price = x.Price,
                IdUser = x.IdUser,
                IdDiscount = x.IdDiscount,
                IdRoom = x.IdRoom,
                Stastus= x.Stastus,

            };
        }
    }
}
