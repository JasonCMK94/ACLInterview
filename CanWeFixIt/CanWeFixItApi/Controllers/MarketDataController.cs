using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    using System.Linq;

    using CanWeFixItService;

    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public MarketDataController(IDatabaseService database)
        {
            _database = database;
        }

        // GET
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            IList<Instrument> instruments = _database.Instruments().Result.ToList();
            IList<MarketData> marketData = _database.MarketData().Result
                .Where(x => x.Active)
                .Where(md => instruments.Select(i => i.Sedol).Contains(md.Sedol))
                .ToList();

            IList<MarketDataDto> marketDataDtos = marketData.Select(
                md => new MarketDataDto
                         {
                             Id = md.Id,
                             DataValue = md.DataValue,
                             Active = md.Active,
                             InstrumentId = instruments.FirstOrDefault(i => md.Sedol == i.Sedol)?.Id
                         }).ToList();

            return Ok(marketDataDtos);
        }
    }
}