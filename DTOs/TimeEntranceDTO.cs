namespace EmployeeControl.DTOs
{
    public class TimeEntranceDto
    {
        public string Hour  = DateTime.Now.ToString("HH-mm");
        public string Day = DateTime.Now.ToString("dd-MM-yyyy");
    }
}
