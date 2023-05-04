using System.ComponentModel.DataAnnotations;

namespace EmployeeControl.Entidades
{
    public class Employee : BaseEntity
    {

        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

    }
}
