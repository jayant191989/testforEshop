using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class EmployeeSalary : AuditableEntity<Guid>
    {
        public DateTime Date { get; set; }
        public string FullName { get; set; }
        public decimal? WorkHours { get; set; }
        public decimal? OverTimeHours { get; set; }
        public decimal? WorkHourTotal { get; set; }
        public decimal? RatePerHour { get; set; }
        public decimal? RatePerHourOvertime { get; set; }
        public decimal? OverTimeTotal { get; set; }
        public Guid SalaryId { get; set; }
        public virtual Salary Salary { get; set; }
    }
}
