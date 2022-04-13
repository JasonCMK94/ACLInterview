using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    using System.Linq;

    using CanWeFixItService;

    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("v1/instruments")]
    public class InstrumentController : ControllerBase
    {
        private readonly CanWeFixItContext _context;
        
        public InstrumentController(CanWeFixItContext context)
        {
            _context = context;
        }
        
        // GET
        public async Task<ActionResult<IEnumerable<Instrument>>> Get()
        {
            var instruments = await _context.Instrument.Where(x => x.Active).ToListAsync();
            return Ok(instruments);
        }
    }
}