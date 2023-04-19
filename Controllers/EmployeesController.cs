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
        public async Task<ActionResult<IEnumerable<Employee>>> GetV2(string name)
        {
            // trae datos de todas las filas que contengan ese nombre
 
            return await context.Employees.Where(s => s.Fullname.Contains(name)).ToListAsync();
        }



        [HttpPut("{id:int}")] //modelo desconectado
        public async Task<ActionResult> Put(int id, EmployeeDTO employeeDTO)
        {
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

        //[HttpPatch("{id:int}")]
        //public async Task<ActionResult> Patch(int id, EmployeeDTO employeeDTO)
        //{
            
        //    var employee = new Employee
        //    {
        //        Email = employeeDTO.Email
        //    };
        //    employee.Id = id;
        //    context.Update(employee);
        //    await context.SaveChangesAsync();
        //    return Ok();

        //}

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
    }
}
