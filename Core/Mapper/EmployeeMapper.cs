using EmployeeControl.Core.Models.DTOs;
using EmployeeControl.Entidades;
using System.Drawing;

namespace EmployeeControl.Core.Mapper
{
    public class EmployeeMapper
    {
        public static Employee ToEmployee(CreateEmployeeDTO employeeDTO)
        {
            if (employeeDTO != null)
            {
                Employee employee = new()
                {
                    Fullname = employeeDTO.Fullname,
                    Email = employeeDTO.Email
                };
               
                return employee;
            }
            return null;

        }


        public static List<EmployeeDTO> ToEmployeesList(List<Employee> employees)
        {
            List<EmployeeDTO> employeeDto = new();

            if(employees != null)
            {
                foreach (var e in employees)
                {
                    employeeDto.Add
                    (
                        new EmployeeDTO
                        {
                            Fullname = e.Fullname,
                            Email = e.Email
                        }
                    ); ;
                }
                return employeeDto;
            }
            return null;
        }

        public static Employee UpdateToEmployee(Employee employee, CreateEmployeeDTO employeeDTO)
        {

            if (employee != null)
            {
                employee.Fullname = employeeDTO.Fullname;
                employee.Email = employeeDTO.Email;
                return employee;
            }
            return null;
        }

        public static EmployeeDTO GetToEmployee(Employee employee)
        {
            if (employee != null)
            {
                var employeeDto = new EmployeeDTO
                {
                    Fullname = employee.Fullname,
                    Email = employee.Email
                };
                return employeeDto;
            }
            return null;
        }
    }
}

