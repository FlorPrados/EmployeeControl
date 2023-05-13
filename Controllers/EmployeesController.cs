using EmployeeControl.Core.Interfaces;
using EmployeeControl.Core.Models.DTOs;
using EmployeeControl.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeControl.Repositorio.Interfaces;
using System;
using Newtonsoft.Json;

namespace EmployeeControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase   // or Controller ??
    {
        private readonly IEmployeesBusiness _employeesBusiness;

        public EmployeesController(IEmployeesBusiness employeesBusiness)
        {
            _employeesBusiness = employeesBusiness;
        }

        [HttpPost]
         public async Task<IActionResult> Create(CreateEmployeeDTO dto) => Ok(await _employeesBusiness.Create(dto));


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _employeesBusiness.GetAll());
        

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int Id) => Ok(await _employeesBusiness.GetById(Id));



        [HttpPut("id")]
        public async Task<IActionResult> Update(int Id, CreateEmployeeDTO employee) => Ok(await _employeesBusiness.Update(Id, employee));


        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int Id) => Ok(await _employeesBusiness.Delete(Id));


        //[HttpGet("{id}/horarios")]
        //public async Task<IActionResult> GetSchedule(int id)
        //{
       
        //    var employee = await context.Employees.FindAsync(id);
        //    if (employee is null)
        //    {
        //        return NotFound();
        //    }

        //    var entrances = context.TimeEntrances
        //        .Include(b => b.employee)
        //        .Where(x => x.EmployeeId == id).ToList();

        //    var exits = context.TimeExits
        //        .Include(b => b.employee)
        //        .Where(x => x.EmployeeId == id).ToList();

        //    var employeeSchedule = new EmployeeScheduleDto
        //    {
        //        FullName = employee.Fullname,
        //        Email = employee.Email
        //    };

        //    entrances.ForEach(x =>
        //    {
        //        employeeSchedule.TimeEntrances.Add(new TimeEntranceDto
        //        {
        //            Day = x.Day,
        //            Hour = x.Hour,
        //        });
        //    });

        //    exits.ForEach(x =>
        //    {
        //        employeeSchedule.TimeExits.Add(new TimeExitDto
        //        {
        //            Day = x.Day,
        //            Hour = x.Hour,

        //        });
        //    });


        //    return Ok(employeeSchedule);

        //}
    }
}
