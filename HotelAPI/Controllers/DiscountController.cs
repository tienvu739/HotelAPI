using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        [HttpGet("dsAllDiscount")]
        public IActionResult dsAllDiscount()
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                List<CDiscount> ds = context.Discounts.Select(h => CDiscount.chuyendoi(h)).ToList();
                return Ok(ds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("adddiscount")]
        public IActionResult adddiscount(CDiscount x)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Discount ex = context.Discounts.FirstOrDefault(h => h.IdDiscount == x.IdDiscount);
                if (ex != null)
                {
                    return BadRequest(new { message = "Khuyến mãi đã tồn tại" });
                }
                Discount a = CDiscount.chuyendoi(x);
                context.Discounts.Add(a);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("edit")]
        public IActionResult editDiscount(CDiscount x)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Discount a = context.Discounts.Find(x.IdDiscount);
                if (a == null)
                {
                    return NotFound(new { message = "Khuyến mãi không tồn tại" });
                }

                a.NameDiscount = x.NameDiscount;
                a.DescribeDiscount = x.DescribeDiscount;
                a.DiscountAmount = x.DiscountAmount;
                a.DiscountNumber = x.DiscountNumber;
                context.SaveChanges();
                return Ok(new { message = "Cập nhật khuyến mãi thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deletDiscount")]
        public IActionResult deletDiscount(string id)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                Discount x = context.Discounts.Find(id);
                if (x == null)
                {
                    return NotFound(new { message = "Khuyến mãi không tồn tại" });
                }
                context.Discounts.Remove(x);
                context.SaveChanges();
                return Ok(new { message = "Xóa khuyến mãi thành công" });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
