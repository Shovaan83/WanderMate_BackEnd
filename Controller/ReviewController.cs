using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.context;
using FirstWeb.Dtos;
using FirstWeb.Dtos.ReviewDTOs;
using FirstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Review>>> Get()
        {
            try
            {
                var review = await _context.Reviews.ToListAsync();

                var getReviewDto = review.Select(review => new GetReviewDto{
                    Id = review.Id,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
            });

            return Ok(getReviewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<Review>>> Create([FromBody] ReviewDto reviewDto)
        {
            try
            {
                if(reviewDto == null)
                {
                    return BadRequest("Review Data is null");
                }

                var postreviewDto = new Review
                {
                    Rating = reviewDto.Rating,
                    ReviewText = reviewDto.ReviewText,
                    HotelId = reviewDto.HotelId
                };
                _context.Reviews.AddAsync(postreviewDto);
                await _context.SaveChangesAsync();
                return Ok("Review Created Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<IEnumerable<Review>>> Delete(int id)
        {
            try
            {
                var review = _context.Reviews.Find(id);
                if (review == null)
                {
                    return NotFound();
                }
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return Ok("Review Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<Review>>> Update(int id, [FromBody] ReviewDto reviewDto)
        {
            try
            {
                if(reviewDto == null)
                {
                    return NotFound();
                }

                var find_review = await _context.Reviews.FindAsync(id);

                if(find_review == null)
                {
                     return NotFound();
                }

                find_review.Rating = reviewDto.Rating;
                find_review.ReviewText = reviewDto.ReviewText;

                await _context.SaveChangesAsync();
                return Ok("Review Updated Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}