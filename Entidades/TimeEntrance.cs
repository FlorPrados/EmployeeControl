namespace EmployeeControl.Entidades
{
    public class TimeEntrance
    {
        public int Id { get; set; }
        public string Hour { get; set; }
        public string Day { get; set; }
        public int EmployeeId { get; set; }
        public Employee employee { get; set; } = null!;
    }

}
