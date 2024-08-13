using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThingsToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThingsToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult> Get()
        {
            try
            {
                var thingsToDo = await _context.ThingsToDos.ToListAsync();
                Console.WriteLine(thingsToDo);
                return Ok(thingsToDo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}