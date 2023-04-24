using EmployeeControl.DTOs;
using EmployeeControl.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmployeeControl.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO employeeDTO)
        {

            var ExistingEmail = await context.Employees.AnyAsync(e => e.Email == employeeDTO.Email);
            if (ExistingEmail)
            {
                return BadRequest("Ya existe un empleado con el mail " + employeeDTO.Email);
            }
            var employee = new Employee

            {
                Fullname = employeeDTO.Fullname,
                Email = employeeDTO.Email
            };
            context.Add(employee);     //.add trabaja con entidades creadas, entonces no puedo enviarle dentro un DTO, porque no cree esa entidad
            await context.SaveChangesAsync();    //Aquì se guarda el empleado generado en la tabla Employee
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await context.Employees.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetV2(int id)
        {
            var idFound = await context.Employees.AnyAsync(e => e.Id == id);
            if (!idFound)
            {
                return NotFound();
            }
            return await context.Employees.Where(e => e.Id == id).ToListAsync();
        }



        [HttpPut("{id:int}")] //modelo desconectado
        public async Task<ActionResult> Put(int id, EmployeeDTO employeeDTO)
        {
            var idFound = await context.Employees.AnyAsync(e => e.Id == id);
            if (!idFound)
            {
                return NotFound();
            }
            var employee = new Employee
            {
                Fullname = employeeDTO.Fullname,
                Email = employeeDTO.Email
            };
            employee.Id = id;
            context.Update(employee);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(s => s.Id == id);
            if (employee is null)
            {
                return NotFound();
            }
            context.Remove(employee);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{id}/horarios")]
        public async Task<ActionResult<IEnumerable<EmployeeScheduleDto>>> GetSchedule(int id)
        {
            //Get por empleado, que traiga tambien la data de entradas y salidas, y que las que pertenecen a el empleado, se mapeen en las propiedades de EmployeeScheduleDto 
            var employee = await context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound();
            }

            var entrances = context.TimeEntrances
                .Include(b => b.employee)
                .Where(x => x.EmployeeId == id).ToList();

            var exits = context.TimeExits
                .Include(b => b.employee)
                .Where(x => x.EmployeeId == id).ToList();


            List<TimeEntranceDto> eachEntrance = new();
            List<TimeExitDto> eachExit = new();

            entrances.ForEach(x =>
            {
                eachEntrance.Add(new TimeEntranceDto
                {
                    Day = x.Day,
                    Hour = x.Hour,
                });
            });

            exits.ForEach(x =>
            {
                eachExit.Add(new TimeExitDto
                {
                    Day = x.Day,
                    Hour = x.Hour,
                });
            });



            var employeeSchedule = new EmployeeScheduleDto
            {
                FullName = employee.Fullname,
                Email = employee.Email,
                TimeExits = exits.
            }
            return Ok();

        }
    }
}
