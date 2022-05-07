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
    public class StocksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStock()
        {
            return await _context.Stock.ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(string id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        [HttpGet("GetStocksLastTwenty")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocksLastTwenty()
        {
            var result = await _context.Stock.OrderByDescending(x => x.DateAdded).Take(20).ToListAsync();
            return result;
        }

        [HttpGet("GetStockByStockNameLike/{stockName}")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStockByStockNameLike(string stockName)
        {
            var result = await _context.Stock.Where(x => x.StockName.ToUpper().Contains(stockName.ToUpper())).ToListAsync();
            return result;
        }

        [HttpGet("GetStockByStockName/{stockName}")]
        public async Task<ActionResult<Stock>> GetStockByStockName(string stockName)
        {
            var result = await _context.Stock.Where(x => x.StockName.ToUpper().Equals(stockName.ToUpper())).FirstOrDefaultAsync();
            return result;
        }

        [HttpGet("EnoughStocksAvailable/{stockName}/{stockQuantity}")]
        public async Task<object> EnoughStocksAvailable(string stockName, int stockQuantity)
        {
            var result = await _context.Stock.FindAsync(stockName);
            return result.StockQuantity >= stockQuantity;
        }

        [HttpGet("GetStocksByOwner/{stockOwner}")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocksByOwner(string stockOwner)
        {
            var result = await _context.Stock.Where(x => x.StockOwner.Equals(stockOwner)).OrderByDescending(x => x.DateAdded).ToListAsync();
            return result;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(string id, Stock stock)
        {
            if (id != stock.StockName)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            _context.Stock.Add(stock);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StockExists(stock.StockName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStock", new { id = stock.StockName }, stock);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(string id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExists(string id)
        {
            return _context.Stock.Any(e => e.StockName == id);
        }
    }
}
