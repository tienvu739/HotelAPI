using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Service
    {
        public Service()
        {
            Hotelservices = new HashSet<Hotelservice>();
        }

        public string IdService { get; set; } = null!;
        public string? NameService { get; set; }

        public virtual ICollection<Hotelservice> Hotelservices { get; set; }
    }
}
