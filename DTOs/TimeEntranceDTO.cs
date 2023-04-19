﻿namespace EmployeeControl.DTOs
{
    public class TimeEntranceDTO
    {
        public string Hour  = DateTime.Now.ToString("HH-mm");
        public string Day = DateTime.Now.ToString("dd-MM-yyyy");
        public int EmployeeId { get; set; }
    }
}