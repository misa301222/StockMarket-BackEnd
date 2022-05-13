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
    public class StockBoughtsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockBoughtsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StockBoughts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockBought>>> GetStockBought()
        {
            return await _context.StockBought.ToListAsync();
        }

        [HttpGet("GetFrequentlyBoughtStocks")]
        public async Task<ActionResult<IEnumerable<StockBought>>> GetFrequentlyBoughtStocks()
        {
            var dateToday = DateTime.Today;
            var stockBought = await _context.StockBought.Where(x => x.TransactionDate.ToUniversalTime().Date.Equals(dateToday.ToUniversalTime().Date)).GroupBy(x => x.StockName).Select(
                x => x.OrderByDescending(x => x.QuantityBought).First()).ToListAsync();

            return stockBought;
        }

        [HttpGet("GetStockBoughtByEmailAndStockName/{email}/{stockName}")]
        public async Task<ActionResult<IEnumerable<StockBought>>> GetStockBoughtByEmailAndStockName(string email, string stockName)
        {
            var stockBought = await _context.StockBought.Where(x => x.Email.Equals(email) && x.StockName.Equals(stockName)).OrderByDescending(x => x.TransactionDate).ToListAsync();
            return stockBought;
        }

        // GET: api/StockBoughts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockBought>> GetStockBought(int id)
        {
            var stockBought = await _context.StockBought.FindAsync(id);

            if (stockBought == null)
            {
                return NotFound();
            }

            return stockBought;
        }

        // PUT: api/StockBoughts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockBought(int id, StockBought stockBought)
        {
            if (id != stockBought.StocksBoughtId)
            {
                return BadRequest();
            }

            _context.Entry(stockBought).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockBoughtExists(id))
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

        // POST: api/StockBoughts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockBought>> PostStockBought(StockBought stockBought)
        {
            _context.StockBought.Add(stockBought);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockBought", new { id = stockBought.StocksBoughtId }, stockBought);
        }

        // DELETE: api/StockBoughts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockBought(int id)
        {
            var stockBought = await _context.StockBought.FindAsync(id);
            if (stockBought == null)
            {
                return NotFound();
            }

            _context.StockBought.Remove(stockBought);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockBoughtExists(int id)
        {
            return _context.StockBought.Any(e => e.StocksBoughtId == id);
        }
    }
}
