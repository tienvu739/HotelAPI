using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet("dsAllService")]
        public IActionResult dsAllService()
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                List<CService> ds = context.Services.Select(h => CService.chuyendoi(h)).ToList();
                return Ok(ds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
    }
}
