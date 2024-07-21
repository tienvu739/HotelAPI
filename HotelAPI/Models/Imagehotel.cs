using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Imagehotel
    {
        public string IdImageHotel { get; set; } = null!;
        public byte[]? ImageData { get; set; }
        public string? IdHotel { get; set; }

        public virtual Hotel? IdHotelNavigation { get; set; }
    }
}
