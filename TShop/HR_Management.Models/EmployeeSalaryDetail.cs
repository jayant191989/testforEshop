using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class EmployeeSalaryDetail : AuditableEntity<Guid>
    {
        public bool? ESI { get; set; }
        public bool? PF { get; set; }
        public bool? Enrolled { get; set; }
        public string Status { get; set; }
        public decimal? RatePerHour { get; set; }
        public decimal? RatePerHourOvertime { get; set; }
        public decimal? FixedSalary { get; set; }
        public string OverTimeCal { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
