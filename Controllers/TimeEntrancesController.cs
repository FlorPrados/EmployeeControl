using EmployeeControl.DTOs;
using EmployeeControl.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Controllers
{
    [ApiController]
    [Route("api/TimeEntrances")]
    public class TimeEntrancesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TimeEntrancesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TimeEntranceDTO timeEntranceDTO)
        {
            var alreadyAssignedId = await context.TimeEntrances.AnyAsync(t => t.EmployeeId == timeEntranceDTO.EmployeeId && t.Day == timeEntranceDTO.Day);
            // t especifica un determinado registro en la tabla TimeEntrances

            if (alreadyAssignedId) 
            {
                return BadRequest("Ya se le ha asignado un horario de entrada al empleado con ID " + timeEntranceDTO.EmployeeId);
            }


            var timeEntrance = new TimeEntrance

            {
                EmployeeId = timeEntranceDTO.EmployeeId,
                Day = timeEntranceDTO.Day,
                Hour = timeEntranceDTO.Hour
            }; 
            
            context.Add(timeEntrance);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeEntrance>>> Get()
        {
            return await context.TimeEntrances.OrderBy(time => time.Day ).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TimeEntrance>> Get2(int id)
        {
            var record = await context.TimeEntrances
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
            var timeentrance = await context.TimeEntrances.FirstOrDefaultAsync(s => s.Id == id);
            if (timeentrance is null)
            {
                return NotFound();
            }
            context.Remove(timeentrance);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
