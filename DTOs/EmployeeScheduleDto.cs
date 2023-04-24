namespace EmployeeControl.DTOs
{
    public class EmployeeScheduleDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<TimeEntranceDto> TimeEntrances { get; set; } = new List<TimeEntranceDto>();
        public List<TimeExitDto> TimeExits { get; set; } = new List<TimeExitDto>();
    }
}
