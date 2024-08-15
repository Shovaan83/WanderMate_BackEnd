using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos;
using FirstWeb.Dtos.HotelDto;
using FirstWeb.Migrations;
using FirstWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class HotelController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]


        public async Task<ActionResult<IEnumerable<Hotel>>> Get()
        {
            try
            {
                var hotels = await _context.Hotels.ToListAsync();
                // Console.WriteLine(hotels);
                // return Ok(hotels);
                var getHotelDto = hotels.Select(hotel => new GetHotelDto{
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Description = hotel.Description,
                    Price = hotel.Price,
                    ImageUrl = hotel.ImageUrl,
                    FreeCancellation = hotel.FreeCancellation,
                    ReserveNow = hotel.ReserveNow
                });

                return Ok(getHotelDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Hotel>>> Create([FromBody] HotelDtos hoteldto)
        {
            try
            {
                if (hoteldto == null)
                {
                    return BadRequest("Hotel Data is null");
                }

                var postHotelDto = new Hotel{
                    Name = hoteldto.Name,
                    Description = hoteldto.Description,
                    Price = hoteldto.Price,
                    ImageUrl = hoteldto.ImageUrls,
                    FreeCancellation = hoteldto.FreeCancellation,
                    ReserveNow = hoteldto.ReserveNow
                };

                _context.Hotels.Add(postHotelDto);
                await _context.SaveChangesAsync();
                return Ok("Hotel Created Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok(hotel);
        }


        // public IActionResult Create([FromBody] Hotel hotel)
        // {

        //     if (hotel == null)
        //     {
        //         return NotFound(); // 404 not found to
        //     }
        //     return Ok(hotel);
        // }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<Hotel>>> GetById(int id)
        {
            try
            {
                var hotel = await _context.Hotels.FindAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> Delete(int id)
        {
            try
            {
                var hotel = _context.Hotels.Find(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return Ok("Hotel Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<Hotel>>> Update(int id,[FromBody] HotelDtos hotelDto)
        {
            try
            {

                if (hotelDto == null)
                {
                    return NotFound();
                }

                var find_hotel = await _context.Hotels.FindAsync(id);

                if (find_hotel == null)
                {
                    return NotFound();
                }
                find_hotel.Name = hotelDto.Name;
                find_hotel.Description = hotelDto.Description;
                find_hotel.Price = hotelDto.Price;
                find_hotel.ImageUrl = hotelDto.ImageUrls;
                find_hotel.FreeCancellation = hotelDto.FreeCancellation;
                find_hotel.ReserveNow = hotelDto.ReserveNow;


                await _context.SaveChangesAsync();
                return Ok("Hotel Updated Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Hotel>>> SearchByName([FromQuery] string name)
        {
            try
            {
                var hotels = await _context.Hotels
    .Where(h => h.Name.Contains(name))
    .ToListAsync();

                if (!hotels.Any())
                {
                    return NotFound();
                }

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
