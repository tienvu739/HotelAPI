namespace HotelAPI.Models
{
    public class CUser
    {
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? NameUser { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public static CUser chuyendoi(User x)
        {
            if (x == null) return null;
            return new CUser
            {
                Account = x.Account,
                Password = x.Password,
                NameUser = x.NameUser,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            };
        }
        public static User chuyendoi(CUser x)
        {
            if (x == null) return null;
            return new User
            {
                Account = x.Account,
                Password = x.Password,
                NameUser = x.NameUser,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            };
        }
    }
}
