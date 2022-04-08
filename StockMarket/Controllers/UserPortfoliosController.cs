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
    public class UserPortfoliosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPortfoliosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPortfolios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPortfolio>>> GetUserPortfolios()
        {
            return await _context.UserPortfolios.ToListAsync();
        }

        // GET: api/UserPortfolios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPortfolio>> GetUserPortfolio(string id)
        {
            var userPortfolio = await _context.UserPortfolios.FindAsync(id);

            if (userPortfolio == null)
            {
                return NotFound();
            }

            return userPortfolio;
        }

        [HttpGet("GetUserPortfolioByEmail/{email}")]
        public async Task<ActionResult<IEnumerable<UserPortfolio>>> GetUserPortfolioByEmail(string email)
        {
            var userPortfolio = await _context.UserPortfolios.Where(x => x.Email.Equals(email)).ToListAsync();
            return userPortfolio;
        }

        [HttpGet("GetUserPortfolioByEmailAndStockName/{email}/{stockName}")]
        public async Task<ActionResult<UserPortfolio>> GetUserPortfolioByEmailAndStockName(string email, string stockName)
        {
            var userPortfolio = await _context.UserPortfolios.Where(x => x.Email.Equals(email) && x.StockName.Equals(stockName)).FirstOrDefaultAsync();
            return userPortfolio;
        }

        // PUT: api/UserPortfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPortfolio(string id, UserPortfolio userPortfolio)
        {
            if (id != userPortfolio.Email)
            {
                return BadRequest();
            }

            _context.Entry(userPortfolio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPortfolioExists(id))
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

        [HttpPut("UpdateUserPortfolio/{email}/{stockName}")]
        public async Task<IActionResult> UpdateUserPortfolio(string email, string stockName, UserPortfolio userPortfolio)
        {
            if (email != userPortfolio.Email || stockName != userPortfolio.StockName)
            {
                return BadRequest();
            }

            _context.Entry(userPortfolio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPortfolioExists(email) || !UserPortfolioExists(stockName))
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

        // POST: api/UserPortfolios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPortfolio>> PostUserPortfolio(UserPortfolio userPortfolio)
        {
            _context.UserPortfolios.Add(userPortfolio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserPortfolioExists(userPortfolio.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserPortfolio", new { id = userPortfolio.Email }, userPortfolio);
        }

        // DELETE: api/UserPortfolios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPortfolio(string id)
        {
            var userPortfolio = await _context.UserPortfolios.FindAsync(id);
            if (userPortfolio == null)
            {
                return NotFound();
            }

            _context.UserPortfolios.Remove(userPortfolio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPortfolioExists(string id)
        {
            return _context.UserPortfolios.Any(e => e.Email == id);
        }
    }
}
