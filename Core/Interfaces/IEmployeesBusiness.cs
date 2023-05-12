using EmployeeControl.Core.Models;
using EmployeeControl.Core.Models.DTOs;

namespace EmployeeControl.Core.Interfaces
{
    public interface IEmployeesBusiness
    {

        Task<Response<bool>> Create(CreateEmployeeDTO dto);
        Task<Response<List<EmployeeDTO>>> GetAll();
        Task<Response<EmployeeDTO>> GetById(int Id);
        Task<Response<bool>> Delete(int Id);
        Task<Response<bool>> Update(EmployeeDTO employeeDTO, int Id);

    }
}
