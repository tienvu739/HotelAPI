using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Evaluate
    {
        public string IdEvaluate { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public string? Title { get; set; }
        public string? DescribeEvaluate { get; set; }
        public int? Point { get; set; }
        public string? IdHotel { get; set; }
        public string? IdUser { get; set; }

        public virtual Hotel? IdHotelNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}
