using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixItApi.Controllers
{
    using CanWeFixItService;

    [ApiController]
    [Route("v1/valuations")]
    public class ValuationController : ControllerBase
    {
        private readonly IDatabaseService _database;

        private const string _marketValuationName = "DataValueTotal";

        public ValuationController(IDatabaseService database)
        {
            _database = database;
        }

        // GET
        public async Task<ActionResult<IEnumerable<MarketValuation>>> Get()
        {
            MarketValuation valuation = new MarketValuation
                                            {
                                                Name = _marketValuationName,
                                                Total = _database.MarketData().Result
                                                    .Where(x => x.Active)
                                                    .Sum(x => x.DataValue.Value)
                                            };

            return new List<MarketValuation> { valuation };
        }
    }
}
