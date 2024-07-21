namespace HotelAPI.Models
{
    public class CDiscount
    {
        public string IdDiscount { get; set; } = null!;
        public string? NameDiscount { get; set; }
        public string? DescribeDiscount { get; set; }
        public double? DiscountAmount { get; set; }
        public int? DiscountNumber { get; set; }

        public static CDiscount chuyendoi(Discount x)
        {
            if (x == null) return null;
            return new CDiscount
            {
                IdDiscount = x.IdDiscount,
                NameDiscount = x.NameDiscount,
                DescribeDiscount = x.DescribeDiscount,
                DiscountAmount = x.DiscountAmount,
                DiscountNumber = x.DiscountNumber
            };
        }
        public static Discount chuyendoi(CDiscount x)
        {
            if (x == null) return null;
            return new Discount
            {
                IdDiscount = x.IdDiscount,
                NameDiscount = x.NameDiscount,
                DescribeDiscount = x.DescribeDiscount,
                DiscountAmount = x.DiscountAmount,
                DiscountNumber = x.DiscountNumber
            };
        }
    }
}
