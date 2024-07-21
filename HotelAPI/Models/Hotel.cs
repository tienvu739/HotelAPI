using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Evaluates = new HashSet<Evaluate>();
            Hotelconvenients = new HashSet<Hotelconvenient>();
            Hotelservices = new HashSet<Hotelservice>();
            Imagehotels = new HashSet<Imagehotel>();
            Rooms = new HashSet<Room>();
        }

        public string IdHotel { get; set; } = null!;
        public string? NameHotel { get; set; }
        public string? AddressHotel { get; set; }
        public string? DescribeHotel { get; set; }
        public string? PolicyHotel { get; set; }
        public string? TypeHotel { get; set; }
        public bool? StatusHotel { get; set; }
        public string? IdHotelier { get; set; }

        public virtual Hotelier? IdHotelierNavigation { get; set; }
        public virtual ICollection<Evaluate> Evaluates { get; set; }
        public virtual ICollection<Hotelconvenient> Hotelconvenients { get; set; }
        public virtual ICollection<Hotelservice> Hotelservices { get; set; }
        public virtual ICollection<Imagehotel> Imagehotels { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
