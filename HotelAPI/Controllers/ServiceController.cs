using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet("services/{idHotel}")]
        public IActionResult GetHotelServices(string idHotel)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var services = _context.Hotelservices
                    .Where(hs => hs.IdHotel == idHotel)
                    .Select(hs => new
                    {
                        hs.IdServiceNavigation.IdService,
                        hs.IdServiceNavigation.NameService
                    })
                    .ToList();

                if (services == null || services.Count == 0)
                {
                    return NotFound(new { message = "Không có dịch vụ nào cho khách sạn này." });
                }

                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpPost("addHotelService")]
        public IActionResult AddHotelService([FromBody] HotelServiceDto hotelServiceDto)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                if (hotelServiceDto == null || string.IsNullOrEmpty(hotelServiceDto.IdService) || string.IsNullOrEmpty(hotelServiceDto.IdHotel))
                {
                    return BadRequest(new { message = "Dữ liệu dịch vụ khách sạn không hợp lệ." });
                }

                Hotelservice hotelService = new Hotelservice
                {
                    IdHotelServer = Guid.NewGuid().ToString(),
                    IdService = hotelServiceDto.IdService,
                    IdHotel = hotelServiceDto.IdHotel
                };

                _context.Hotelservices.Add(hotelService);
                _context.SaveChanges();

                return Ok(new { message = "Dịch vụ đã được thêm thành công cho khách sạn." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpPost("addService")]
        public IActionResult addService(CService x)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Service ex = context.Services.FirstOrDefault(h => h.IdService == x.IdService);
                if (ex != null)
                {
                    return BadRequest(new { message = "Dịch vụ đã tồn tại" });
                }
                Service a = CService.chuyendoi(x);
                context.Services.Add(a);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("edit")]
        public IActionResult editService(CService x)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Service a = context.Services.Find(x.IdService);
                if (a == null)
                {
                    return NotFound(new { message = "Dịch vụ  không tồn tại" });
                }
                a.NameService = x.NameService;
                context.SaveChanges();
                return Ok(new { message = "Cập nhật dịch vụ  thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deletService")]
        public IActionResult deletService(string id)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Service x = context.Services.Find(id);
                if (x == null)
                {
                    return NotFound(new { message = "Dịch vụ  không tồn tại" });
                }
                context.Services.Remove(x);
                context.SaveChanges();
                return Ok(new { message = "Xóa dịch vụ  thành công" });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("dsServicer")]
        public IActionResult listService()
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                List<CService> services = context.Services.Select(h => CService.chuyendoi(h)).ToList();
                return Ok(services);
            }catch (Exception ex) { 
                return BadRequest(ex); 
            }
        }
    }
}
