using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos;
using FirstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Get()
        {
            try
            {
                var bookings = await _context.Bookings
                .Include(b => b.User)
                // .Include(b => b.Hotel)
                .ToListAsync();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<Booking>>> Create([FromBody] BookingDto bookingDto)
        {
            try
            {
                var booking = new Booking
                {
                    BookingDate = bookingDto.BookingDate,
                    UserId = bookingDto.UserId,
                    // HotelId = bookingDto.HotelId
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}