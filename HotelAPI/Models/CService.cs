namespace HotelAPI.Models
{
    public class CService
    {
        public string IdService { get; set; } = null!;
        public string? NameService { get; set; }
        public static CService chuyendoi(Service x)
        {
            if (x == null) return null;
            return new CService() { IdService = x.IdService, NameService = x.NameService };
        }
        public static Service chuyendoi(CService x)
        {
            if (x == null) return null;
            return new Service() { IdService = x.IdService, NameService = x.NameService };
        }
    }
}
