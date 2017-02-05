using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class Salary : AuditableEntity<Guid>
    {
        public DateTime Date { get; set; }      
        public decimal? DaySalary { get; set; }
        public virtual List<EmployeeSalary> EmployeeSalary { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
