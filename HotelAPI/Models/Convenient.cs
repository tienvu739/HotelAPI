using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Convenient
    {
        public Convenient()
        {
            Hotelconvenients = new HashSet<Hotelconvenient>();
        }

        public string IdConvenient { get; set; } = null!;
        public string? NameConvenient { get; set; }

        public virtual ICollection<Hotelconvenient> Hotelconvenients { get; set; }
    }
}
