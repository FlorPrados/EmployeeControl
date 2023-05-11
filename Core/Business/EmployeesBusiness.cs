using EmployeeControl.Core.Interfaces;
using EmployeeControl.Core.Models;
using EmployeeControl.Core.Models.DTOs;
using EmployeeControl.Repositorio;
using EmployeeControl.Repositorio.Interfaces;

namespace EmployeeControl.Core.Bussiness
{
    public class EmployeesBusiness : IEmployeesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Create(List<CreateEmployeeDTO> employeeDto)
        {
            var response = new Response<bool>(await _unitOfWork.EmployeesRepository);

            if (!response.Data)
            {
                response.Succeeded = false;
                response.Message = ResponseMessage.UnexpectedErrors;
            }
            return response;
        }
    }
}
