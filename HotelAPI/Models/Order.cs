using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Order
    {
        public string IdOrder { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public double? Price { get; set; }
        public string? IdUser { get; set; }
        public string? IdDiscount { get; set; }
        public string? IdRoom { get; set; }

        public virtual Discount? IdDiscountNavigation { get; set; }
        public virtual Room? IdRoomNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}
