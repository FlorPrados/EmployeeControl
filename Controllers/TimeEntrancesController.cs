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

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var employee = context.Employees.FirstOrDefault(s => s.Id == id);
            var today = DateTime.Now.ToString("dd-MM-yyyy");
            var alreadyAssignedId = context.TimeEntrances.Any(t => t.EmployeeId == id && t.Day == today);
            // t especifica un determinado registro en la tabla TimeEntrances

            if (alreadyAssignedId || employee is null) 
            {
                return NotFound();
            }

            var timeEntrance = new TimeEntrance
            {
                EmployeeId = id,
                Day = DateTime.Now.ToString("dd-MM-yyyy"),
                Hour = DateTime.Now.ToString("HH-mm")
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

        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<TimeEntrance>>> GetV2(int id)
        {
            var idFound = await context.TimeEntrances.AnyAsync(e => e.EmployeeId == id);
            if (!idFound)
            {
                return NotFound();
            }
            return await context.TimeEntrances.Where(e => e.EmployeeId == id).ToListAsync();
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
            return Ok();
        }
    }
}
