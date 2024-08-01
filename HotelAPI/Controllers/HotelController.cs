using HotelAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        [Authorize]
        [HttpGet("dsHotel")]
        public IActionResult dsHotel()
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                List<CHotel> ds = _context.Hotels
                    .Where(h => h.StatusHotel == true && h.TypeHotel == "Khách sạn")
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

                if (ds == null || ds.Count == 0)
                {
                    return BadRequest(new { message = "Không có khách sạn nào có trạng thái hoạt động và loại là 'khách sạn'." });
                }

                return Ok(ds);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpGet("dsHomestay")]
        public IActionResult dsHomestay()
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                List<CHotel> ds = _context.Hotels
                    .Where(h => h.StatusHotel == true && h.TypeHotel == "Nhà nghỉ")
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

                if (ds == null || ds.Count == 0)
                {
                    return BadRequest(new { message = "Không có khách sạn nào có trạng thái hoạt động và loại là 'khách sạn'." });
                }

                return Ok(ds);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("dsAllHotel")]
        public IActionResult dsAllHotel(string idhotelier)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                List<CHotel> ds = _context.Hotels
                    .Where(h => h.IdHotelier == idhotelier)
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

                if (ds == null || ds.Count == 0)
                {
                    return BadRequest(new { message = "Không tồn tại khách sạn cho idhotelier này." });
                }

                return Ok(ds);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }

        [Authorize]
        [HttpPost("addHotel")]
        public IActionResult AddHotel([FromBody] HotelDto hotelDto)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                if (hotelDto == null)
                {
                    return BadRequest(new { message = "Invalid hotel data." });
                }

                var hotel = new Hotel
                {
                    IdHotel = Guid.NewGuid().ToString(),
                    NameHotel = hotelDto.NameHotel,
                    AddressHotel = hotelDto.AddressHotel,
                    DescribeHotel = hotelDto.DescribeHotel,
                    PolicyHotel = hotelDto.PolicyHotel,
                    TypeHotel = hotelDto.TypeHotel,
                    StatusHotel = hotelDto.StatusHotel,
                    IdHotelier = hotelDto.IdHotelier
                };

                foreach (var image in hotelDto.Images)
                {
                    hotel.Imagehotels.Add(new Imagehotel
                    {
                        IdImageHotel = Guid.NewGuid().ToString(),
                        ImageData = Convert.FromBase64String(image.ImageData),
                        IdHotel = hotel.IdHotel
                    });
                }

                _context.Hotels.Add(hotel);
                _context.SaveChanges();

                return Ok(new { message = "Hotel added successfully.", idHotel = hotel.IdHotel });
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Error: " + innerExceptionMessage });
            }
        }
        [Authorize]
        [HttpPost("nameHotel")]
        public IActionResult NameHotel(string id)
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                var hotels = context.Hotels
                    .Where(u => u.IdHotelier == id && u.StatusHotel == true)
                    .Select(h => new CNameHotel
                    {
                        IdHotel = h.IdHotel,
                        NameHotel = h.NameHotel
                    })
                    .ToList();

                if (hotels == null || hotels.Count == 0)
                {
                    return BadRequest(new { message = "Không tồn tại" });
                }

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("idHotel")]
        public IActionResult GetHotelById(string idHotel)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                CHotel hotel = _context.Hotels
                    .Where(h => h.IdHotel == idHotel && h.StatusHotel == true)
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
                    .FirstOrDefault();

                if (hotel == null)
                {
                    return NotFound(new { message = "Không tìm thấy khách sạn với id này hoặc khách sạn không hoạt động." });
                }

                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpGet("searchHotels")]
        public IActionResult SearchHotels(
        [FromQuery] string? name = null,
        [FromQuery] string? address = null,
        [FromQuery] double? minPrice = null,
        [FromQuery] double? maxPrice = null,
        [FromQuery] string? roomType = null)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var query = _context.Hotels.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(h => h.NameHotel.Contains(name));
                }

                if (!string.IsNullOrEmpty(address))
                {
                    query = query.Where(h => h.AddressHotel.Contains(address));
                }

                if (minPrice.HasValue || maxPrice.HasValue || !string.IsNullOrEmpty(roomType))
                {
                    query = query.Where(h => h.Rooms.Any(r =>
                        (!minPrice.HasValue || r.Price >= minPrice.Value) &&
                        (!maxPrice.HasValue || r.Price <= maxPrice.Value) &&
                        (string.IsNullOrEmpty(roomType) || r.TypeRoom.Contains(roomType))
                    ));
                }

                var hotels = query.Select(h => new CHotel
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
                }).ToList();

                if (!hotels.Any())
                {
                    return NotFound(new { message = "Không tìm thấy khách sạn nào phù hợp." });
                }

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }


        [HttpGet("QLKS")]
        public IActionResult allKS()
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                var hotel = context.Hotels.Select(i => new CHotel
                {
                    IdHotel = i.IdHotel,
                    NameHotel = i.NameHotel,
                    AddressHotel = i.AddressHotel,
                    DescribeHotel = i.DescribeHotel,
                    PolicyHotel = i.PolicyHotel,
                    TypeHotel = i.TypeHotel,
                    StatusHotel = i.StatusHotel,
                    IdHotelier = i.IdHotelier,
                }).ToList();
                return Ok(hotel);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ApproveHotel")]
        public IActionResult ApproveHotel(string idHotel)
        {
            QLHOTELContext _context = new QLHOTELContext();
            if (string.IsNullOrEmpty(idHotel))
            {
                return BadRequest(new { message = "IdHotel không hợp lệ." });
            }

            try
            {
                // Tìm khách sạn theo IdHotel
                var hotel = _context.Hotels.FirstOrDefault(h => h.IdHotel == idHotel);
                if (hotel == null)
                {
                    return NotFound(new { message = "Khách sạn không tồn tại." });
                }

                // Kiểm tra trạng thái hiện tại
                if (hotel.StatusHotel == true)
                {
                    return BadRequest(new { message = "Khách sạn đã được duyệt." });
                }

                // Cập nhật trạng thái khách sạn
                hotel.StatusHotel = true;
                _context.SaveChanges();

                return Ok(new { message = "Khách sạn đã được duyệt thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
    }
}
