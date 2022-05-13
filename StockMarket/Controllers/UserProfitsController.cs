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
    public class UserProfitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserProfitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfit>>> GetUserProfit()
        {
            return await _context.UserProfit.ToListAsync();
        }

        // GET: api/UserProfits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfit>> GetUserProfit(string id)
        {
            var userProfit = await _context.UserProfit.FindAsync(id);

            if (userProfit == null)
            {
                return NotFound();
            }

            return userProfit;
        }

        // PUT: api/UserProfits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfit(string id, UserProfit userProfit)
        {
            if (id != userProfit.Email)
            {
                return BadRequest();
            }

            _context.Entry(userProfit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfitExists(id))
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

        // POST: api/UserProfits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProfit>> PostUserProfit(UserProfit userProfit)
        {
            _context.UserProfit.Add(userProfit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserProfitExists(userProfit.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserProfit", new { id = userProfit.Email }, userProfit);
        }

        // DELETE: api/UserProfits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfit(string id)
        {
            var userProfit = await _context.UserProfit.FindAsync(id);
            if (userProfit == null)
            {
                return NotFound();
            }

            _context.UserProfit.Remove(userProfit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfitExists(string id)
        {
            return _context.UserProfit.Any(e => e.Email == id);
        }
    }
}
