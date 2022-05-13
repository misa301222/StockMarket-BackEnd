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
    public class StockSoldsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockSoldsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StockSolds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockSold>>> GetStockSold()
        {
            return await _context.StockSold.ToListAsync();
        }

        // GET: api/StockSolds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockSold>> GetStockSold(int id)
        {
            var stockSold = await _context.StockSold.FindAsync(id);

            if (stockSold == null)
            {
                return NotFound();
            }

            return stockSold;
        }

        [HttpGet("GetStockBoughtByEmailAndStockName/{email}/{stockName}")]
        public async Task<ActionResult<IEnumerable<StockSold>>> GetStockBoughtByEmailAndStockName(string email, string stockName)
        {
            var stockSold = await _context.StockSold.Where(x => x.Email.Equals(email) && x.StockName.Equals(stockName)).OrderByDescending(x => x.TransactionDate).ToListAsync();
            return stockSold;
        }

        // PUT: api/StockSolds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockSold(int id, StockSold stockSold)
        {
            if (id != stockSold.StocksSoldId)
            {
                return BadRequest();
            }

            _context.Entry(stockSold).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockSoldExists(id))
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

        // POST: api/StockSolds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockSold>> PostStockSold(StockSold stockSold)
        {
            _context.StockSold.Add(stockSold);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockSold", new { id = stockSold.StocksSoldId }, stockSold);
        }

        // DELETE: api/StockSolds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockSold(int id)
        {
            var stockSold = await _context.StockSold.FindAsync(id);
            if (stockSold == null)
            {
                return NotFound();
            }

            _context.StockSold.Remove(stockSold);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockSoldExists(int id)
        {
            return _context.StockSold.Any(e => e.StocksSoldId == id);
        }
    }
}
