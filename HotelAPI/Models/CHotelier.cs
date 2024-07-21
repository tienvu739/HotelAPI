namespace HotelAPI.Models
{
    public class CHotelier
    {
        public string? NameHotelier { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public static CHotelier chuyendoi(Hotelier x)
        {
            if (x == null) return null;
            return new CHotelier
            {
                NameHotelier = x.NameHotelier,
                Account = x.Account,
                Password = x.Password,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            };
        }
        public static Hotelier chuyendoi(CHotelier x)
        {
            if (x == null) return null;
            return new Hotelier
            {
                NameHotelier = x.NameHotelier,
                Account = x.Account,
                Password = x.Password,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            };
        }
    }
}
