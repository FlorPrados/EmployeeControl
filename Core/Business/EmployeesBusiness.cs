using EmployeeControl.Core.Interfaces;
using EmployeeControl.Core.Mapper;
using EmployeeControl.Core.Models;
using EmployeeControl.Core.Models.DTOs;
using EmployeeControl.Entidades;
using EmployeeControl.Repositorio;
using EmployeeControl.Repositorio.Interfaces;
using System.Collections.Generic;

namespace EmployeeControl.Core.Bussiness
{
    public class EmployeesBusiness : IEmployeesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Create(CreateEmployeeDTO dto)
        {
            var response = new Response<bool>(await _unitOfWork.EmployeesRepository.Create(EmployeeMapper.ToEmployee(dto)));

            if (!response.Data)
            {
                response.Message = ResponseMessage.NotFoundOrDeleted;
                response.Succeeded = false;
            }
            return response;
        }

        public async Task<Response<bool>> Delete(int Id)
        {
            var response = new Response<bool>(await _unitOfWork.EmployeesRepository.Delete(Id));
            if (!response.Data)
            {
                response.Message = ResponseMessage.NotFoundOrDeleted;
                response.Succeeded = false;
            }
            return response;
        }

        public async Task<Response<List<EmployeeDTO>>> GetAll()
        {
            var response = new Response<List<EmployeeDTO>>(EmployeeMapper.ToEmployeesList(await _unitOfWork.EmployeesRepository.GetAll()));

            if (response.Data == null)
            {
                response.Succeeded = false;
                response.Message = ResponseMessage.UnexpectedErrors;
            }

            return response;
        }
           

        public async Task<Response<EmployeeDTO>> GetById(int Id)
        {
            var response = new Response<EmployeeDTO>(EmployeeMapper.GetToEmployee(await _unitOfWork.EmployeesRepository.GetById(Id)));

            if (response.Data == null)
            {
                response.Succeeded = false;
                response.Message = ResponseMessage.NotFoundOrDeleted;
            }

            return response;
        }

        public async Task<Response<bool>> Update(int Id, CreateEmployeeDTO employeeDTO)
        {

            var response = new Response<bool>(false);    // ? 
            var employee = await _unitOfWork.EmployeesRepository.GetById(Id);

            if (employee == null)
            {
                response.Message = ResponseMessage.NotFoundOrDeleted;
                response.Succeeded = false;
                return response;
            }

            response.Data = await _unitOfWork.EmployeesRepository.Update(EmployeeMapper.UpdateToEmployee(employee, employeeDTO));

            return response;
        }
    }
}
