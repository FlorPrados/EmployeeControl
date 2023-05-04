using System.ComponentModel.DataAnnotations;

namespace EmployeeControl.Entidades
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
