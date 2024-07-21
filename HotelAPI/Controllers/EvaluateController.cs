using HotelAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluateController : ControllerBase
    {
        [Authorize]
        [HttpPost("addEvaluate")]
        public IActionResult AddEvaluate([FromBody] EvaluateDto evaluateDto)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                if (evaluateDto == null)
                {
                    return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ." });
                }

                Evaluate evaluate = new Evaluate
                {
                    IdEvaluate = Guid.NewGuid().ToString(),
                    DateCreated = DateTime.Now,
                    Title = evaluateDto.Title,
                    DescribeEvaluate = evaluateDto.DescribeEvaluate,
                    Point = evaluateDto.Point,
                    IdHotel = evaluateDto.IdHotel,
                    IdUser = evaluateDto.IdUser
                };

                _context.Evaluates.Add(evaluate);
                _context.SaveChanges();

                return Ok(new { message = "Đánh giá đã được thêm thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpGet("hotelsWithoutComment")]
        public IActionResult GetHotelsWithoutComment(string idUser)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var userOrders = _context.Orders
                    .Where(o => o.IdUser == idUser)
                    .Select(o => o.IdRoom)
                    .ToList();

                var hotelsOrdered = _context.Rooms
                    .Where(r => userOrders.Contains(r.IdRoom))
                    .Select(r => r.IdHotel)
                    .Distinct()
                    .ToList();

                var hotelsWithComments = _context.Evaluates
                    .Where(e => e.IdUser == idUser && hotelsOrdered.Contains(e.IdHotel))
                    .Select(e => e.IdHotel)
                    .Distinct()
                    .ToList();

                var hotelsWithoutComments = _context.Hotels
                    .Where(h => hotelsOrdered.Contains(h.IdHotel) && !hotelsWithComments.Contains(h.IdHotel))
                    .Select(h => new CHotel
                    {
                        IdHotel = h.IdHotel,
                        NameHotel = h.NameHotel,
                        AddressHotel = h.AddressHotel,
                        DescribeHotel = h.DescribeHotel,
                        PolicyHotel = h.PolicyHotel,
                        TypeHotel = h.TypeHotel,
                        StatusHotel = h.StatusHotel,
                        IdHotelier = h.IdHotelier,
                        Images = h.Imagehotels.Select(i => new CImagehotel
                        {
                            IdImageHotel = i.IdImageHotel,
                            ImageData = i.ImageData
                        }).ToList()
                    })
                    .ToList();

                return Ok(hotelsWithoutComments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpGet("getUserHotelsWithComments")]
        public IActionResult GetUserHotelsWithComments(string idUser)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var hotelIdsWithOrders = _context.Orders
                    .Where(o => o.IdUser == idUser)
                    .Select(o => o.IdRoomNavigation.IdHotel)
                    .Distinct()
                    .ToList();

                var hotelIdsWithComments = _context.Evaluates
                    .Where(e => e.IdUser == idUser)
                    .Select(e => e.IdHotel)
                    .Distinct()
                    .ToList();

                var combinedHotelIds = hotelIdsWithOrders.Intersect(hotelIdsWithComments).ToList();

                var hotels = _context.Hotels
                    .Where(h => combinedHotelIds.Contains(h.IdHotel))
                    .Select(h => new CHotel
                    {
                        IdHotel = h.IdHotel,
                        NameHotel = h.NameHotel,
                        AddressHotel = h.AddressHotel,
                        DescribeHotel = h.DescribeHotel,
                        PolicyHotel = h.PolicyHotel,
                        TypeHotel = h.TypeHotel,
                        StatusHotel = h.StatusHotel,
                        IdHotelier = h.IdHotelier,
                        Images = h.Imagehotels.Select(i => new CImagehotel
                        {
                            IdImageHotel = i.IdImageHotel,
                            ImageData = i.ImageData
                        }).ToList()
                    })
                    .ToList();

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpGet("totalPoints")]
        public IActionResult GetTotalPoints(string idHotel)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var totalPoints = _context.Evaluates
                    .Where(e => e.IdHotel == idHotel)
                    .Sum(e => e.Point ?? 0);

                return Ok(new { IdHotel = idHotel, TotalPoints = totalPoints });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }

    }
}
