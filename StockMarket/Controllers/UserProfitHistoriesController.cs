#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Data.Entity;

namespace StockMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfitHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserProfitHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfitHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfitHistory>>> GetUserProfitHistories()
        {
            return await _context.UserProfitHistories.ToListAsync();
        }

        // GET: api/UserProfitHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfitHistory>> GetUserProfitHistory(int id)
        {
            var userProfitHistory = await _context.UserProfitHistories.FindAsync(id);

            if (userProfitHistory == null)
            {
                return NotFound();
            }

            return userProfitHistory;
        }

        [HttpGet("GetUserProfitHistoryByEmail/{email}")]
        public async Task<ActionResult<IEnumerable<UserProfitHistory>>> GetUserProfitHistories(string email)
        {
            var userProfitHistory = await _context.UserProfitHistories.Where(x => x.Email.Equals(email)).OrderByDescending(x => x.UserProfitId).ToListAsync();
            return userProfitHistory;
        }

        // PUT: api/UserProfitHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfitHistory(int id, UserProfitHistory userProfitHistory)
        {
            if (id != userProfitHistory.UserProfitId)
            {
                return BadRequest();
            }

            _context.Entry(userProfitHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfitHistoryExists(id))
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

        // POST: api/UserProfitHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProfitHistory>> PostUserProfitHistory(UserProfitHistory userProfitHistory)
        {
            _context.UserProfitHistories.Add(userProfitHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProfitHistory", new { id = userProfitHistory.UserProfitId }, userProfitHistory);
        }

        // DELETE: api/UserProfitHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfitHistory(int id)
        {
            var userProfitHistory = await _context.UserProfitHistories.FindAsync(id);
            if (userProfitHistory == null)
            {
                return NotFound();
            }

            _context.UserProfitHistories.Remove(userProfitHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfitHistoryExists(int id)
        {
            return _context.UserProfitHistories.Any(e => e.UserProfitId == id);
        }
    }
}
