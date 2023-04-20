using EmployeeControl.DTOs;
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
        public async Task<IActionResult> Post(TimeExitDTO timeExitDTO)
        {
            var alreadyAssigned = await context.TimeExits.AnyAsync(t => t.EmployeeId == timeExitDTO.EmployeeId && t.Day == timeExitDTO.Day);
            if (alreadyAssigned) 
            {
                return BadRequest("Ya se le ha asignado un horario de salida al empleado con ID " + timeExitDTO.EmployeeId);
            }

            var timeEntrance = await context.TimeEntrances
                .SingleOrDefaultAsync(entrance => entrance.EmployeeId == timeExitDTO.EmployeeId && entrance.Day == timeExitDTO.Day);
            if (timeEntrance == null)
            {
                return BadRequest($"El empleado con ID {timeExitDTO.EmployeeId} no ha registado una entrada");
            }

            var timeExit = new TimeExit

            {
                EmployeeId = timeExitDTO.EmployeeId,
                Day = timeExitDTO.Day,
                Hour = timeExitDTO.Hour
            };

            context.Add(timeExit);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeEntrance>>> Get()
        {
            return await context.TimeEntrances.OrderBy(time => time.Day).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TimeExit>> Get2(int id)
        {
            var record = await context.TimeExits
                 .Include(rec => rec.Hour)
                 .Include(rec => rec.employee)
                     .ThenInclude(emp => emp.Fullname)
                 .FirstOrDefaultAsync(rec => rec.Id == id);

            if (record is null)
            {
                return NotFound();
            }
            return record;
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

