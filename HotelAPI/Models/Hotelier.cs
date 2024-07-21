using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Hotelier
    {
        public Hotelier()
        {
            Hotels = new HashSet<Hotel>();
        }

        public string IdHotelier { get; set; } = null!;
        public string? NameHotelier { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
