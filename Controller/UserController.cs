using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos;
using FirstWeb.Dtos.UserDTOs;
using FirstWeb.Migrations;
using FirstWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
            
                var getUserDto = users.Select(users => new GetUserDto{
                    
                    Id = users.Id,
                    Username = users.Username,
                    Email = users.Email,
                    Role = users.Role,
                    Password = users.Password,
                });

                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<User>>> Create([FromBody] UserDto userDto)
        {
            try{

                var DoesEmailExist = await _context.Users.SingleOrDefaultAsync(x => x.Email == userDto.Email);
                if(DoesEmailExist != null)
                {
                    return BadRequest("Email Already Exists");
                }

                var DoesUsernameExist = await  _context.Users.SingleOrDefaultAsync(x => x.Username == userDto.Username);
                if(DoesUsernameExist != null)
                {
                    return BadRequest("Username Already Exists");
                }

                if(userDto.Password != userDto.ConfrimPassword)
                {
                    return BadRequest("Password does not match.");
                }

                var HashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                var user = new User
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Role = userDto.Role,
                    Password = HashedPassword,
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok("User Created Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<IEnumerable<User>>> Delete(int id)
        {
            try{
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok("User Deleted Succesfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<User>>> Update (int id, [FromBody] UserDto userDto)

        {
            try
            {
                if(userDto == null)
                {
                    return NotFound();
                }

                var find_user = await _context.Users.FindAsync(id);

                if(find_user == null)
                {
                    return NotFound();
                }

                find_user.Username = userDto.Username;
                find_user.Email = userDto.Email;
                find_user.Password = userDto.Password;
                find_user.ConfrimPassword = userDto.ConfrimPassword;

                await _context.SaveChangesAsync();
                return Ok("User Updated Succesfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}