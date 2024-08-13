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
    public class TravelPackagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TravelPackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelPackages>>> Get()
        {
            try
            {
                var travelPackages = await _context.TravelPackages.ToListAsync();
                return Ok(travelPackages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        // public async Task<ActionResult<IEnumerable<TravelPackagesDto>>> Create([FromBody] TravelPackagesDto travelPackagesDto)
        // {
        //     try
        //     {

        //         var travelPackages = new TravelPackages
        //         {
        //             Name = travelPackagesDto.Name,
        //             Description = travelPackagesDto.Description,
        //             Price = travelPackagesDto.Price,
        //             ImageUrl = travelPackagesDto.ImageUrl,
        //         };

        //         _context.TravelPackages.Add(travelPackages);
        //         await _context.SaveChangesAsync();
        //         return Ok("TravelPackages Created Succesfully");
        //     }
        //     catch (Exception ex)
        //     {

        //         return BadRequest(ex.Message);
        //     }
        // }

        public async Task<ActionResult<IEnumerable<TravelPackages>>> Create([FromBody] TravelPackages travelPackages)
        {
            try
            {
                if (travelPackages == null)
                {
                    return BadRequest("TravelPackages data is null");
                }
                await _context.TravelPackages.AddAsync(travelPackages);
                await _context.SaveChangesAsync();
                return Ok("TravelPackages Created Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<TravelPackages>>> Update(int id, TravelPackages updateTravelPackages)
        {
            try
            {
                var find_travelPackages = await _context.TravelPackages.FindAsync(id);

                if (find_travelPackages == null)
                {
                    return NotFound();
                }

                find_travelPackages.Name = updateTravelPackages.Name;
                find_travelPackages.Description = updateTravelPackages.Description;
                find_travelPackages.Price = updateTravelPackages.Price;
                find_travelPackages.ImageUrl = updateTravelPackages.ImageUrl;
                find_travelPackages.FreeCancellation = updateTravelPackages.FreeCancellation;
                find_travelPackages.ReserveNow = updateTravelPackages.ReserveNow;

                await _context.SaveChangesAsync();
                return Ok("TravelPackages Updated Succesfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<IEnumerable<TravelPackages>>> Delete(int id)
        {
            try
            {
                var travelPackages = _context.TravelPackages.Find(id);
                if (travelPackages == null)
                {
                    return NotFound();
                }
                _context.TravelPackages.Remove(travelPackages);
                await _context.SaveChangesAsync();
                return Ok("TravelPackages Deleted Succesfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<TravelPackages>>> GetById(int id)
        {
            try
            {
                var travelPackages = await _context.TravelPackages.FindAsync(id);
                if (travelPackages == null)
                {
                    return NotFound();
                }
                return Ok(travelPackages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}