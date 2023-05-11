using System.ComponentModel.DataAnnotations;

namespace EmployeeControl.Core.Models.DTOs
{
    public class CreateEmployeeDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Fullname { get; set; } = null!;

        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; } = null!;
    }
}
