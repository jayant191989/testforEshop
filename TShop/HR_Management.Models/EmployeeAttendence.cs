using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class EmployeeAttendence : AuditableEntity<Guid>
    {
        public DateTime Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
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
