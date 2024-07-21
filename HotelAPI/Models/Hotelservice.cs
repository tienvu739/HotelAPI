using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Hotelservice
    {
        public string IdHotelServer { get; set; } = null!;
        public string? IdService { get; set; }
        public string? IdHotel { get; set; }

        public virtual Hotel? IdHotelNavigation { get; set; }
        public virtual Service? IdServiceNavigation { get; set; }
    }
}
