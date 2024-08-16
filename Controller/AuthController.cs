using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos.UserDTOs;
using FirstWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("SignIn")]

        public async Task<ActionResult> SignIn([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

                if (user == null)
                {
                    return BadRequest("User not found");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

                if (!isPasswordValid)
                {
                    return BadRequest("Invalid Password");
                }

                var token = _tokenService.GenerateToken(user);

                var response = new{
                    Token = token,
                    Role = user.Role,
                    ExpiresIn = DateTime.Now.AddMinutes(30),
                };

                // HttpContext.Session.SetString("token", token);
                // HttpContext.Session.SetString("Id", user.Id.ToString());
                // HttpContext.Session.SetString("Username", user.Username);
                // HttpContext.Session.SetString("Role", user.Role);

                return Ok(new { Message = "Login Successful", response});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}