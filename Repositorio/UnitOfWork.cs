using EmployeeControl.Entidades;
using EmployeeControl.Repositorio.Interfaces;

namespace EmployeeControl.Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Employee> _EmployeesRepository;
        public IGenericRepository<TimeEntrance> _TimeEntrancesRepository;
        public IGenericRepository<TimeExit> _TimeExitsRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Employee> EmployeesRepository
        {
            get
            {
                if (_EmployeesRepository == null)
                {
                    _EmployeesRepository = new GenericRepository<Employee>(_context);

                }
                return _EmployeesRepository;
            }
        }

        public IGenericRepository<TimeEntrance> TimeEntrancesRepository
        {
            get
            {
                if (_TimeEntrancesRepository == null)
                {
                    _TimeEntrancesRepository = new GenericRepository<TimeEntrance>(_context);

                }
                return _TimeEntrancesRepository;
            }
        }

        public IGenericRepository<TimeExit> TimeExitsRepository
        {
            get
            {
                if (_TimeExitsRepository == null)
                {
                    _TimeExitsRepository = new GenericRepository<TimeExit>(_context);

                }
                return _TimeExitsRepository;
            }
        }
    }
}
