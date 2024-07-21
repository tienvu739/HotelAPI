using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class User
    {
        public User()
        {
            Evaluates = new HashSet<Evaluate>();
            Orders = new HashSet<Order>();
        }

        public string IdUser { get; set; } = null!;
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? NameUser { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Evaluate> Evaluates { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
