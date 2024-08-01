using HotelAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [Authorize]
        [HttpPost("dsRoom")]
        public IActionResult dsRoom(string idhotelier)
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                var hotels = context.Hotels
                    .Where(h => h.IdHotelier == idhotelier)
                    .Select(h => h.IdHotel)
                    .ToList();

                var rooms = context.Rooms
                    .Where(r => hotels.Contains(r.IdHotel))
                    .Select(r => new CRoom
                    {
                        IdRoom = r.IdRoom,
                        NameRoom = r.NameRoom,
                        AreaRoom = r.AreaRoom,
                        People = r.People,
                        PolicyRoom = r.PolicyRoom,
                        BedNumber = r.BedNumber,
                        StatusRoom = r.StatusRoom,
                        TypeRoom = r.TypeRoom,
                        Price = r.Price,
                        IdHotel = r.IdHotel,
                        Imageroms = r.Imagerooms.Select(i => new CImageRoom
                        {
                            IdImageRoom = i.IdImageRoom,
                            ImageData = i.ImageData
                        }).ToList()
                    })
                    .ToList();

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("addRoom")]
        public IActionResult addRoom([FromBody] RoomDTO roomdto) 
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                if (roomdto == null)
                    return NotFound(new { message = "Invalid hotel data " });
                var room = new Room
                {
                    IdRoom = Guid.NewGuid().ToString(),
                    NameRoom = roomdto.NameRoom,
                    AreaRoom = roomdto.AreaRoom,
                    People = roomdto.People,
                    PolicyRoom = roomdto.PolicyRoom,
                    BedNumber = roomdto.BedNumber,
                    StatusRoom = roomdto.StatusRoom,
                    TypeRoom = roomdto.TypeRoom,
                    Price = roomdto.Price,
                    IdHotel = roomdto.IdHotel,
                };
                foreach(var image in roomdto.Images)
                {
                    room.Imagerooms.Add(new Imageroom
                    {
                        IdImageRoom = Guid.NewGuid().ToString(),
                        ImageData = Convert.FromBase64String(image.ImageData),
                        IdRoom = room.IdRoom,
                    });
                }
                context.Rooms.Add(room);
                context.SaveChanges();
                return Ok(new { message = "Hotel added successfully." });
            }catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Error: " + innerExceptionMessage });
            }
        }
        [Authorize]
        [HttpGet("idHotel")]
        public IActionResult idHotel(string idHotel)
        {
            QLHOTELContext context = new QLHOTELContext();
            try
            {
                var rooms = context.Rooms
                    .Where(r => r.IdHotel == idHotel)
                    .Select(r => new CRoom
                    {
                        IdRoom = r.IdRoom,
                        NameRoom = r.NameRoom,
                        AreaRoom = r.AreaRoom,
                        People = r.People,
                        PolicyRoom = r.PolicyRoom,
                        BedNumber = r.BedNumber,
                        StatusRoom = r.StatusRoom,
                        TypeRoom = r.TypeRoom,
                        Price = r.Price,
                        IdHotel = r.IdHotel,
                        Imageroms = r.Imagerooms.Select(i => new CImageRoom
                        {
                            IdImageRoom = i.IdImageRoom,
                            ImageData = i.ImageData
                        }).ToList()
                    })
                    .ToList();

                if (rooms == null || rooms.Count == 0)
                {
                    return NotFound(new { message = "Không tìm thấy phòng nào cho idHotel này." });
                }
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpPut("toggleStatus")]
        public IActionResult ToggleStatus(string idRoom)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var room = _context.Rooms.FirstOrDefault(r => r.IdRoom == idRoom);
                if (room == null)
                {
                    return NotFound(new { message = "Room not found" });
                }

                room.StatusRoom = !room.StatusRoom;
                _context.SaveChanges();

                return Ok(new { message = "Room status updated successfully", status = room.StatusRoom });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        [HttpGet("getHotelNameByRoomId")]
        public IActionResult GetHotelNameByRoomId(string idRoom)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                // Tìm phòng dựa trên idRoom
                var room = _context.Rooms.FirstOrDefault(r => r.IdRoom == idRoom);
                if (room == null)
                {
                    return NotFound(new { message = "Room not found" });
                }

                // Tìm khách sạn tương ứng với phòng
                var hotel = _context.Hotels.FirstOrDefault(h => h.IdHotel == room.IdHotel);
                if (hotel == null)
                {
                    return NotFound(new { message = "Hotel not found for this room" });
                }

                // Trả về tên khách sạn
                return Ok(new { hotelName = hotel.NameHotel });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
    }
}
