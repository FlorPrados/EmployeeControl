using EmployeeControl.Core.DTOs;
using EmployeeControl.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Controllers
{
    [ApiController]
    [Route("api/TimeExits")]
    public class TimeExitsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TimeExitsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int id)
        {
            var today = DateTime.Now.ToString("dd-MM-yyyy");
            var timeEntrance = await context.TimeEntrances
                .SingleOrDefaultAsync(entrance => entrance.EmployeeId == id && entrance.Day == today);

            var alreadyAssigned = await context.TimeExits.AnyAsync(t => t.EmployeeId == id && t.Day == today);
            if (alreadyAssigned || timeEntrance == null) 
            {
                return NotFound();
            }

            var timeExit = new TimeExit
            {
                EmployeeId = id,
                Day = DateTime.Now.ToString("dd-MM-yyyy"),
                Hour = DateTime.Now.ToString("HH-mm")
            };

            context.Add(timeExit);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeExit>>> Get()
        {
            return await context.TimeExits.OrderBy(time => time.Day).ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<TimeExit>>> GetV2(int id)
        {
            var idFound = await context.TimeExits.AnyAsync(e => e.EmployeeId == id);
            if (!idFound)
            {
                return NotFound();
            }
            return await context.TimeExits.Where(e => e.EmployeeId == id).ToListAsync();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var timeexit = await context.TimeExits.FirstOrDefaultAsync(s => s.Id == id);
            if (timeexit is null)
            {
                return NotFound();
            }
            context.Remove(timeexit);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}


// SingleOrDefaultAsync:
// Es un método de consulta de Entity Framework que busca un ùnico registro que cumpla con una condición específica.
// En este caso, se está buscando un TimeEntrance que tenga el mismo EmployeeId y Day que el TimeExit que se está intentando agregar.

