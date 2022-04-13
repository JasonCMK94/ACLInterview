using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    using System.Linq;

    using CanWeFixItService;

    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly CanWeFixItContext _context;

        public MarketDataController(CanWeFixItContext context)
        {
            _context = context;
        }

        // GET
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            // Query market data inner joined to instruments via sedol field
            var marketDataDtos = await _context.MarketData.Where(x => x.Active).Join(
                                     _context.Instrument,
                                     md => md.Sedol,
                                     i => i.Sedol,
                                     (md, i) => new MarketDataDto
                                                    {
                                                        Id = md.Id,
                                                        DataValue = md.DataValue,
                                                        Active = md.Active,
                                                        InstrumentId = i.Id
                                                    }).ToListAsync();

            return Ok(marketDataDtos);
        }
    }
}