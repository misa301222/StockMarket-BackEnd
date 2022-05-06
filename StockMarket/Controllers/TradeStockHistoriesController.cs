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
    public class TradeStockHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TradeStockHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TradeStockHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TradeStockHistory>>> GetTradeStockHistories()
        {
            return await _context.TradeStockHistories.ToListAsync();
        }

        // GET: api/TradeStockHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TradeStockHistory>> GetTradeStockHistory(int id)
        {
            var tradeStockHistory = await _context.TradeStockHistories.FindAsync(id);

            if (tradeStockHistory == null)
            {
                return NotFound();
            }

            return tradeStockHistory;
        }

        [HttpGet("GetTradeStockHistoriesBySourceEmail/{email}")]
        public async Task<ActionResult<IEnumerable<TradeStockHistory>>> GetTradeStockHistoriesBySourceEmail(string email)
        {
            var tradeStockHistory = await _context.TradeStockHistories.Where(x => x.SourceEmail.Equals(email)).ToListAsync();
            return tradeStockHistory;
        }

        [HttpGet("GetTradeStockHistoriesBySourceEmailAndStatus/{email}/{status}")]
        public async Task<ActionResult<IEnumerable<TradeStockHistory>>> GetTradeStockHistoriesBySourceEmailAndStatus(string email, string status)
        {
            var tradeStockHistory = await _context.TradeStockHistories.Where(x => (x.SourceEmail.Equals(email) || x.DestinyEmail.Equals(email)) && x.Status.Equals(status)).ToListAsync();
            return tradeStockHistory;
        }

        [HttpGet("GetDistinctPendingTradeStockHistoriesBySourceEmail/{email}")]
        public async Task<ActionResult<IEnumerable<TradeStockHistory>>> GetDistinctPendingTradeStockHistoriesBySourceEmail(string email)
        {
            var tradeStockHistory = await _context.TradeStockHistories.Where(x => (x.SourceEmail.Equals(email) || x.DestinyEmail.Equals(email)) && (!x.Status.Equals("PENDING"))).OrderByDescending(x => x.TransactionDate).ToListAsync();
            return tradeStockHistory;
        }

        // PUT: api/TradeStockHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTradeStockHistory(int id, TradeStockHistory tradeStockHistory)
        {
            if (id != tradeStockHistory.TradeStockHistoryId)
            {
                return BadRequest();
            }

            _context.Entry(tradeStockHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeStockHistoryExists(id))
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

        // POST: api/TradeStockHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TradeStockHistory>> PostTradeStockHistory(TradeStockHistory tradeStockHistory)
        {
            _context.TradeStockHistories.Add(tradeStockHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTradeStockHistory", new { id = tradeStockHistory.TradeStockHistoryId }, tradeStockHistory);
        }

        // DELETE: api/TradeStockHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTradeStockHistory(int id)
        {
            var tradeStockHistory = await _context.TradeStockHistories.FindAsync(id);
            if (tradeStockHistory == null)
            {
                return NotFound();
            }

            _context.TradeStockHistories.Remove(tradeStockHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TradeStockHistoryExists(int id)
        {
            return _context.TradeStockHistories.Any(e => e.TradeStockHistoryId == id);
        }
    }
}
