using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Room
    {
        public Room()
        {
            Imagerooms = new HashSet<Imageroom>();
            Orders = new HashSet<Order>();
        }

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

        public virtual Hotel? IdHotelNavigation { get; set; }
        public virtual ICollection<Imageroom> Imagerooms { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
