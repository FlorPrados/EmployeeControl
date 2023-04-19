namespace EmployeeControl.Entidades
{
    public class TimeEntrance
    {
        public int Id { get; set; }
        public string Hour { get; set; } = null!;
        public string Day { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Employee employee { get; set; } = null!;
    }

}
