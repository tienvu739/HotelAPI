using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Hotelconvenient
    {
        public string IdHotelConvenient { get; set; } = null!;
        public string? IdConvenient { get; set; }
        public string? IdHotel { get; set; }

        public virtual Convenient? IdConvenientNavigation { get; set; }
        public virtual Hotel? IdHotelNavigation { get; set; }
    }
}
