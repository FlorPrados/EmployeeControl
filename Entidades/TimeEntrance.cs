using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeControl.Entidades
{
    public class TimeEntrance : BaseEntity
    {
        public string Hour { get; set; }
        public string Day { get; set; }

        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        public Employee employee { get; set; } = null!;

    }

}
