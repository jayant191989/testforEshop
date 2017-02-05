using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class EmployeeAttendenceViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public string TotalTime { get; set; }
        public string AttendenceType { get; set; }
        public decimal? WorkHours { get; set; }
        public decimal? OverTimeHours { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
        public Guid AttendenceId { get; set; }
        public virtual Attendence Attendence { get; set; }
    }
}