using EmployeeControl.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
    }
}
