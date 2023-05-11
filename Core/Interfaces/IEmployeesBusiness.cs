using EmployeeControl.Core.Models;
using EmployeeControl.Core.Models.DTOs;

namespace EmployeeControl.Core.Interfaces
{
    public interface IEmployeesBusiness
    {

        Task<Response<bool>> Create(List<CreateEmployeeDTO> employeeDTO);
        Task<Response<List<EmployeeDTO>>> GetAll();
        Task<Response<List<EmployeeDTO>>> GetById(int Id);
        Task<Response<string>> Delete(int Id);   //string?
        Task<Response<bool>> Update(EmployeeDTO employeeDTO, int Id);

    }
}
