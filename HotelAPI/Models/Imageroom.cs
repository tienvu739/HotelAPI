using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Imageroom
    {
        public string IdImageRoom { get; set; } = null!;
        public byte[]? ImageData { get; set; }
        public string? IdRoom { get; set; }

        public virtual Room? IdRoomNavigation { get; set; }
    }
}
