using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelierController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string Account, string Password)
        {
            try
            {
                using (QLHOTELContext context = new QLHOTELContext())
                {
                    // Tìm người dùng dựa trên Account
                    Hotelier hotelier = context.Hoteliers.FirstOrDefault(u => u.Account == Account);
                    if (hotelier == null)
                    {
                        return BadRequest(new { message = "Tài khoản không tồn tại" });
                    }

                    // Kiểm tra mật khẩu
                    if (hotelier.Password != Password)
                    {
                        return BadRequest(new { message = "Sai mật khẩu" });
                    }

                    // Đăng nhập thành công
                    return Ok(new { message = "Đăng nhập thành công" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpPost("register")]
        public IActionResult register(CHotelier a)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Hotelier existingUser = context.Hoteliers.FirstOrDefault(u => u.Account == a.Account);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Tài khoản đã tồn tại" });
                }
                Hotelier existingEmail = context.Hoteliers.FirstOrDefault(u => u.Email == a.Email);
                if (existingEmail != null)
                {
                    return BadRequest(new { message = "Email đã tồn tại" });
                }

                // Kiểm tra xem số điện thoại đã tồn tại chưa
                Hotelier existingPhoneNumber = context.Hoteliers.FirstOrDefault(u => u.PhoneNumber == a.PhoneNumber);
                if (existingPhoneNumber != null)
                {
                    return BadRequest(new { message = "Số điện thoại đã tồn tại" });
                }
                Hotelier x = CHotelier.chuyendoi(a);
                x.IdHotelier = Guid.NewGuid().ToString();
                context.Hoteliers.Add(x);
                context.SaveChanges();

                // Tạo token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("your_secure_secret_key_here_1234567890!@#$%^");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, x.IdHotelier),
                    new Claim(ClaimTypes.Email, x.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpGet("dsHotelier")]
        public IActionResult dsHoterlier()
        {
            try
            {
                using (QLHOTELContext context = new QLHOTELContext())
                {
                    var hotelier = context.Hoteliers
                        .Select(h => new
                        {
                            h.IdHotelier,
                            CHotelier = CHotelier.chuyendoi(h)
                        })
                        .ToList();

                    var result = hotelier.Select(u => new
                    {
                        u.IdHotelier,
                        u.CHotelier.Account,
                        u.CHotelier.Password,
                        u.CHotelier.NameHotelier,
                        u.CHotelier.Email,
                        u.CHotelier.PhoneNumber
                    }).ToList();

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
    }
}
