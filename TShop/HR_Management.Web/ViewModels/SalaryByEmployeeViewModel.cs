using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class SalaryByEmployeeViewModel
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public string FullName { get; set; }
        public string AttendenceType { get; set; }       
        public double? DaySalary { get; set; }
        public decimal? WorkHours { get; set; }
        public decimal? OverTimeHours { get; set; }
        public decimal? RatePerHour { get; set; }
        public decimal? RatePerHourOvertime { get; set; }
        public decimal? WorkHourTotal { get; set; }
        public decimal? OverTimeTotal { get; set; }
        public decimal? GrandTotalWorkHours { get; set; }
        public decimal? GrandTotalOverTimeHours { get; set; }
    }
}