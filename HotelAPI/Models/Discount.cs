using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Orders = new HashSet<Order>();
        }

        public string IdDiscount { get; set; } = null!;
        public string? NameDiscount { get; set; }
        public string? DescribeDiscount { get; set; }
        public double? DiscountAmount { get; set; }
        public int? DiscountNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
