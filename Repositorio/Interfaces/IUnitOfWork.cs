using EmployeeControl.Entidades;

namespace EmployeeControl.Repositorio.Interfaces
{
    public interface IUnitOfWork
    {

        IGenericRepository<Employee> EmployeesRepository { get; }
        IGenericRepository<TimeEntrance> TimeEntrancesRepository { get; }
        IGenericRepository<TimeExit> TimeExitsRepository { get; }



    }
}
