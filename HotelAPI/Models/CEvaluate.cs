namespace HotelAPI.Models
{
    public class CEvaluate
    {
        public string IdEvaluate { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public string? Title { get; set; }
        public string? DescribeEvaluate { get; set; }
        public int? Point { get; set; }
        public string? IdHotel { get; set; }
        public string? IdUser { get; set; }
        public static CEvaluate chuyendoi(Evaluate x)
        {
            if (x == null) return null;
            return new CEvaluate()
            {
                IdEvaluate = x.IdEvaluate,
                DateCreated = x.DateCreated,
                Title = x.Title,
                DescribeEvaluate = x.DescribeEvaluate,
                Point = x.Point,
                IdHotel = x.IdHotel,
                IdUser = x.IdUser
            };
        }
        public static Evaluate chuyendoi(CEvaluate x)
        {
            if (x == null) return null;
            return new Evaluate()
            {
                IdEvaluate = x.IdEvaluate,
                DateCreated = x.DateCreated,
                Title = x.Title,
                DescribeEvaluate = x.DescribeEvaluate,
                Point = x.Point,
                IdHotel = x.IdHotel,
                IdUser = x.IdUser
            };
        }
    }
}
