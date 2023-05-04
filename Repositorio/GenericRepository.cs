using EmployeeControl.Entidades;
using EmployeeControl.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Repositorio
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        public async Task<bool> Update(T entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Set<T>().Update(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<T>> GetAll() => await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();


        public async Task<T> GetById(int Id) => await (from t in _context.Set<T>() where t.Id == Id && !t.IsDeleted select t).FirstOrDefaultAsync();

        public async Task<bool> Delete(int Id)
        {
            var entity = await GetById(Id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}