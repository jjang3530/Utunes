using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UtunesAPI.Data;
using UtunesAPI.Models;

namespace UtunesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly UtunesContext _context;

        public SongsController(UtunesContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Song.Include(a => a.Reviews).ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _context.Song.Include(a => a.Reviews).FirstOrDefaultAsync(i => i.SongId == id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.SongId)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.Song.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.SongId }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(int id)
        {
            var song = await _context.Song.
                FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            await _context.SaveChangesAsync();

            return song;
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.SongId == id);
        }


        // GET: api/Songs/2/reviews
        [HttpGet("{id:int}/reviews")]
        public async Task<ActionResult> GetReviews(int id)
        {
            var song = await _context.Song
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(r => r.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song.Reviews);
        }

        // GET: api/Songs/2/reviews/1
        [HttpGet("{id:int}/reviews/{reviewId:int}")]
        public async Task<ActionResult<Review>> GetReview(int id, int reviewId)
        {
            var song = await _context.Song
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(s => s.SongId == id);

            if (song == null)
            {
                return NotFound();
            }

            var review = song.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }


        // PUT: api/Songs/2/reviews/1
        [HttpPut("{id:int}/reviews/{reviewId:int}")]
        public async Task<IActionResult> PutReview(int id, int reviewId, Review review)
        {
            if (reviewId != review.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Songs/reviews
        [HttpPost("reviews")]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }



        // DELETE: api/Songs/reviews/1
        [HttpDelete("reviews/{reviewId:int}")]
        public async Task<ActionResult<Review>> DeleteReview(int reviewId)
        {
            var review = await _context.Review.
                FindAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ReviewId == id);
        }

    }
}
