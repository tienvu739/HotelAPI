using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult getDS()
        {
            QLHOTELContext context = new QLHOTELContext();
            var admin = context.Admins.Select(h => new
            {
                h.IdAdmin,
                h.Account,
                h.Password,
                h.Name,
                h.Address,
                h.Role,
            }).ToList();
            return Ok(admin);
        }
        [HttpPost("create")]
        public IActionResult CreateAccount(Admin newAdmin)
        {
            QLHOTELContext _context = new QLHOTELContext();
            if (newAdmin == null || string.IsNullOrEmpty(newAdmin.Account) || string.IsNullOrEmpty(newAdmin.Password) || string.IsNullOrEmpty(newAdmin.Name) || string.IsNullOrEmpty(newAdmin.Role))
            {
                return BadRequest(new { message = "Thông tin tài khoản không hợp lệ." });
            }

            try
            {
                // Kiểm tra tài khoản đã tồn tại hay chưa
                var existingAdmin = _context.Admins.FirstOrDefault(a => a.Account == newAdmin.Account);
                if (existingAdmin != null)
                {
                    return BadRequest(new { message = "Tài khoản đã tồn tại." });
                }

                // Tạo tài khoản mới
                newAdmin.IdAdmin = Guid.NewGuid().ToString();
                _context.Admins.Add(newAdmin);
                _context.SaveChanges();

                return Ok(new { message = "Tài khoản đã được tạo thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpGet("login")]
        public IActionResult login(string account, string password)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Admin admin = context.Admins.FirstOrDefault(h => h.Account == account);
                if (admin == null)
                {
                    return BadRequest(new { message = "Tài khoản không tồn tại" });
                }
                if(admin.Password != password)
                {
                    return BadRequest(new { message = "Sai mật khẩu" });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("your_secure_secret_key_here_1234567890!@#$%^"); // Replace with your secret key

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Account),
                    new Claim(ClaimTypes.Role, admin.Role)
                };
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = tokenString,
                    role = admin.Role
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpDelete("DeleteAccount")]
        public IActionResult DeleteAccount(string id)
        {
            QLHOTELContext _context = new QLHOTELContext();
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "IdAdmin không hợp lệ." });
            }

            try
            {
                var admin = _context.Admins.FirstOrDefault(a => a.IdAdmin == id);
                if (admin == null)
                {
                    return NotFound(new { message = "Tài khoản không tồn tại." });
                }
                _context.Admins.Remove(admin);
                _context.SaveChanges();

                return Ok(new { message = "Tài khoản đã được xóa thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
    }
}
