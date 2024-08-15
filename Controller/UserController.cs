using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos;
using FirstWeb.Dtos.UserDTOs;
using FirstWeb.Migrations;
using FirstWeb.Models;
using FirstWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public UserController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                var getUserDto = users.Select(users => new GetUserDto
                {

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
            try
            {

                var DoesEmailExist = await _context.Users.SingleOrDefaultAsync(x => x.Email == userDto.Email);
                if (DoesEmailExist != null)
                {
                    return BadRequest("Email Already Exists");
                }

                var DoesUsernameExist = await _context.Users.SingleOrDefaultAsync(x => x.Username == userDto.Username);
                if (DoesUsernameExist != null)
                {
                    return BadRequest("Username Already Exists");
                }

                if (userDto.Password != userDto.ConfrimPassword)
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
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok("User Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<User>>> Update(int id, [FromBody] UserDto userDto)

        {
            try
            {
                if (userDto == null)
                {
                    return NotFound();
                }

                var find_user = await _context.Users.FindAsync(id);

                if (find_user == null)
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return BadRequest(new { message = "User not found." });

            var resetToken = Guid.NewGuid().ToString();

            // // Store the reset token with the user's information in a secure way (e.g., in a database)
            _context.PasswordResets.Add(new PasswordReset { Token = resetToken });
            await _context.SaveChangesAsync();

            var emailBody = $"This is your Reset Token: {resetToken}";
            await _emailService.SendEmailAsync(user.Email, "Password Reset", emailBody);

            // Construct reset URL
            // var resetLink = Url.Action("ResetPassword",
            //     "Account",  // Controller name
            //     new { token = resetToken, email = user.Username },  // Query parameters
            //     Request.Scheme);  // Scheme (http or https)

            // var emailBody = $"This is your Reset Token: {resetToken}";



            // Construct the email body
            // var emailBody = $"This is your Reset Token: {resetToken}";
            // Send the email
            // await _emailService.SendEmailAsync(user.Username, "Password Reset", emailBody);

            // Send the reset link via email
            // var emailBody = $"Please reset your password by clicking here: <a href='{resetLink}'>Reset Password</a>";
            // await _emailService.SendEmailAsync(user.Username, "Password Reset", emailBody);

            return Ok(new { message = "If an account with that email exists, a password reset link has been sent." });
        }

        [HttpPost("UpdatePassword")]

        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO model)
        {
            var token = await _context.PasswordResets.FirstOrDefaultAsync(p => p.Token  == model.Token);
            if (token == null)
                return BadRequest(new { message = "User not found." });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return BadRequest(new { message = "User not found." });

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.Password = hashedPassword;
            _context.PasswordResets.Remove(token);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Password updated successfully." });
        }


    }
}