using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixItApi.Controllers
{
    using CanWeFixItService;

    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("v1/valuations")]
    public class ValuationController : ControllerBase
    {
        private const string _marketValuationName = "DataValueTotal";

        private readonly CanWeFixItContext _context;

        public ValuationController(CanWeFixItContext context)
        {
            _context = context;
        }

        // GET
        public async Task<ActionResult<IEnumerable<MarketValuation>>> Get()
        {
            var marketData = await _context.MarketData.Where(x => x.Active).ToListAsync();
            var valuation = new MarketValuation
                                {
                                    Name = _marketValuationName,
                                    Total = marketData.Sum(x => x.DataValue ?? 0)
                                };

            return new List<MarketValuation> { valuation };
        }
    }
}
