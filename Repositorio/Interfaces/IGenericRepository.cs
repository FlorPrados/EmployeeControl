using EmployeeControl.Entidades;

namespace EmployeeControl.Repositorio.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        //Task<List<T>> GetAsync(QueryProperty<T> query); // Necesario para personalizar la creacion de TimeExit

    }
}
