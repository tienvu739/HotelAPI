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
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string Account, string Password)
        {
            try
            {
                using (QLHOTELContext context = new QLHOTELContext())
                {
                    // Tìm người dùng hoặc hotelier dựa trên Account
                    User user = context.Users.FirstOrDefault(u => u.Account == Account);
                    Hotelier hotelier = context.Hoteliers.FirstOrDefault(t => t.Account == Account);

                    if (user == null && hotelier == null)
                    {
                        return BadRequest(new { message = "Tài khoản không tồn tại" });
                    }

                    // Kiểm tra mật khẩu
                    if ((user != null && user.Password != Password) || (hotelier != null && hotelier.Password != Password))
                    {
                        return BadRequest(new { message = "Sai mật khẩu" });
                    }

                    // Đăng nhập thành công
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("your_secure_secret_key_here_1234567890!@#$%^"); // Replace with your secret key

                    var claims = new List<Claim>();

                    if (user != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Name, user.Account.ToString()));
                        claims.Add(new Claim(ClaimTypes.Role, "User"));
                    }
                    else if (hotelier != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Name, hotelier.Account.ToString()));
                        claims.Add(new Claim(ClaimTypes.Role, "Hotelier"));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(1), // Token expiry time
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    if (user != null)
                    {
                        return Ok(new
                        {
                            id = user.IdUser,
                            message = "user",
                            token = tokenString
                        });
                    }
                    else if (hotelier != null)
                    {
                        return Ok(new
                        {
                            id = hotelier.IdHotelier,
                            message = "hotelier",
                            token = tokenString
                        });
                    }

                    return BadRequest(new { message = "Đã xảy ra lỗi" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }

        [HttpPost("register")]
        public IActionResult register(CUser a)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                User existingUser = context.Users.FirstOrDefault(u => u.Account == a.Account);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Tài khoản đã tồn tại"});
                }

                User existingEmail = context.Users.FirstOrDefault(u => u.Email == a.Email);
                if (existingEmail != null)
                {
                    return BadRequest(new { message = "Email đã tồn tại" });
                }

                // Kiểm tra xem số điện thoại đã tồn tại chưa
                User existingPhoneNumber = context.Users.FirstOrDefault(u => u.PhoneNumber == a.PhoneNumber);
                if (existingPhoneNumber != null)
                {
                    return BadRequest(new { message = "Số điện thoại đã tồn tại" });
                }
                User x = CUser.chuyendoi(a);
                x.IdUser = Guid.NewGuid().ToString();
                context.Users.Add(x);
                context.SaveChanges();

                // Tạo token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("your_secure_secret_key_here_1234567890!@#$%^");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, x.IdUser),
                    new Claim(ClaimTypes.Email, x.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new { token = tokenString });
            }catch(Exception ex)
            { return BadRequest(ex.Message);}
        }
        [HttpGet("dsUser")]
        public IActionResult dsUser()
        {
            try
            {
                using (QLHOTELContext context = new QLHOTELContext())
                {
                    var users = context.Users
                        .Select(h => new
                        {
                            h.IdUser,
                            CUser = CUser.chuyendoi(h)
                        })
                        .ToList();

                    var result = users.Select(u => new
                    {
                        u.IdUser,
                        u.CUser.Account,
                        u.CUser.Password,
                        u.CUser.NameUser,
                        u.CUser.Email,
                        u.CUser.PhoneNumber
                    }).ToList();

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpGet("idUser")]
        public IActionResult idUser(string id)
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                User user = context.Users.FirstOrDefault(u => u.IdUser == id);
                if (user == null)
                    return BadRequest(new { message = "không tìm thấy user" });
                return Ok(new
                {
                    Name = user.NameUser,
                    phone = user.PhoneNumber,
                });
            }catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}
