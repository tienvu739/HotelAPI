using HotelAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("getAllOrders")]
        public IActionResult GetAllOrders()
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                List<COrder> orders = _context.Orders
                    .Select(o => new COrder
                    {
                        DateCreated = o.DateCreated,
                        CheckInDate = o.CheckInDate,
                        CheckOutDate = o.CheckOutDate,
                        Price = o.Price,
                        IdUser = o.IdUser,
                        IdDiscount = o.IdDiscount,
                        IdRoom = o.IdRoom
                    })
                    .ToList();

                if (orders == null || orders.Count == 0)
                {
                    return NotFound(new { message = "Không có đơn hàng nào." });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("addOrder")]
        public IActionResult AddOrder([FromBody] COrder orderDto)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                if (orderDto == null)
                {
                    return BadRequest(new { message = "Dữ liệu đơn hàng không hợp lệ." });
                }

                Order order = new Order
                {
                    IdOrder = Guid.NewGuid().ToString(),
                    DateCreated = orderDto.DateCreated ?? DateTime.Now,
                    CheckInDate = orderDto.CheckInDate,
                    CheckOutDate = orderDto.CheckOutDate,
                    Price = orderDto.Price,
                    IdUser = orderDto.IdUser,
                    IdDiscount = orderDto.IdDiscount,
                    IdRoom = orderDto.IdRoom
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                return Ok(new { message = "Đơn hàng đã được thêm thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("getOrdersByHotelier")]
        public IActionResult GetOrdersByHotelier(string idhotelier)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                // Lấy danh sách các khách sạn thuộc về idhotelier
                var hotels = _context.Hotels
                    .Where(h => h.IdHotelier == idhotelier)
                    .ToList(); // Thực hiện truy vấn và chuyển kết quả sang danh sách

                if (hotels == null || hotels.Count == 0)
                {
                    return BadRequest(new { message = "Không có khách sạn nào thuộc về idhotelier này." });
                }

                // Lấy danh sách các phòng thuộc về các khách sạn đó
                var hotelIds = hotels.Select(h => h.IdHotel).ToList(); // Lấy danh sách IdHotel
                var rooms = _context.Rooms
                    .Where(r => hotelIds.Contains(r.IdHotel))
                    .ToList(); // Thực hiện truy vấn và chuyển kết quả sang danh sách

                // Lấy danh sách các đơn hàng thuộc về các phòng đó
                var roomIds = rooms.Select(r => r.IdRoom).ToList(); // Lấy danh sách IdRoom
                var orders = _context.Orders
                    .Where(o => roomIds.Contains(o.IdRoom))
                    .AsEnumerable() // Chuyển sang IEnumerable để thực hiện phía máy khách
                    .Select(o => new COrderDetail
                    {
                        id = o.IdOrder,
                        DateCreated = o.DateCreated,
                        CheckInDate = o.CheckInDate,
                        CheckOutDate = o.CheckOutDate,
                        Price = o.Price,
                        IdUser = o.IdUser,
                        IdDiscount = o.IdDiscount,
                        IdRoom = o.IdRoom,
                        HotelName = hotels.FirstOrDefault(h => h.IdHotel == o.IdRoomNavigation.IdHotel)?.NameHotel,
                        RoomName = rooms.FirstOrDefault(r => r.IdRoom == o.IdRoom)?.NameRoom
                    })
                    .ToList();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost("getOrdersByUserId")]
        public IActionResult GetOrdersByUserId(string idUser)
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var orderDetails = _context.Orders
                    .Where(o => o.IdUser == idUser)
                    .Select(o => new COrderDetail
                    {
                        id = o.IdOrder,
                        DateCreated = o.DateCreated,
                        CheckInDate = o.CheckInDate,
                        CheckOutDate = o.CheckOutDate,
                        Price = o.Price,
                        IdUser = o.IdUser,
                        IdDiscount = o.IdDiscount,
                        IdRoom = o.IdRoom,
                        RoomName = o.IdRoomNavigation.NameRoom,
                        HotelName = o.IdRoomNavigation.IdHotelNavigation.NameHotel
                    })
                    .ToList();

                if (orderDetails == null || orderDetails.Count == 0)
                {
                    return BadRequest(new { message = "Không có đơn hàng nào cho người dùng này." });
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        [HttpGet("dsOrder")]
        public IActionResult dsOrder()
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                var orders = context.Orders.Select(h => new
                {
                    h.IdOrder,
                    h.DateCreated,
                    h.CheckInDate,
                    h.CheckOutDate,
                    h.Price,
                    h.IdUser,
                    h.IdDiscount,
                    h.IdRoom,

                });
                return Ok(orders);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete")]
        public IActionResult delete(string id)
        {
            try
            {
                QLHOTELContext context = new QLHOTELContext();
                var order = context.Orders.FirstOrDefault(o => o.IdOrder == id);
                if (order == null)
                {
                    return NotFound(new { message = "Đơn hàng không tồn tại." });
                }
                context.Orders.Remove(order);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getHotelStatistics")]
        public IActionResult GetHotelStatistics()
        {
            QLHOTELContext _context = new QLHOTELContext();
            try
            {
                var hotelStatistics = _context.Hotels
                    .Select(h => new
                    {
                        HotelId = h.IdHotel,
                        HotelName = h.NameHotel,
                        Rooms = h.Rooms.Select(r => new
                        {
                            Orders = r.Orders.Select(o => new
                            {
                                o.Price,
                                DateYear = o.DateCreated.Value.Year,
                                DateMonth = o.DateCreated.Value.Month
                            })
                        })
                    })
                    .ToList()
                    .Select(h => new
                    {
                        h.HotelId,
                        h.HotelName,
                        TotalRevenue = h.Rooms.SelectMany(r => r.Orders).Sum(o => o.Price ?? 0),
                        TotalOrders = h.Rooms.SelectMany(r => r.Orders).Count(),
                        MonthlyRevenue = h.Rooms
                                          .SelectMany(r => r.Orders)
                                          .GroupBy(o => new { o.DateYear, o.DateMonth })
                                          .Select(g => new
                                          {
                                              Year = g.Key.DateYear,
                                              Month = g.Key.DateMonth,
                                              Revenue = g.Sum(o => o.Price ?? 0),
                                              OrderCount = g.Count()
                                          })
                                          .ToList()
                    })
                    .ToList();

                return Ok(hotelStatistics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
    }
}
