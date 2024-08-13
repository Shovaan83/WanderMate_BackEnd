using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos.UserDTOs;
using FirstWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
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

                return Ok(new{Message= "Login Successful", token=token});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}