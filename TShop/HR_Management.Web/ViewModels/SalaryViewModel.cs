using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class SalaryViewModel
    {
        public int? Id { get; set; }
        public Guid AttendenceId { get; set; }
        public DateTime? Date { get; set; }
        public double? DaySalary { get; set; }
        public decimal? GrandTotalWorkHours { get; set; }
        public decimal? GrandTotalOverTimeHours { get; set; }
        public IEnumerable<EmployeeSalaryViewModel> EmployeeSalaryViewModel { get; set; }   
     

    }
}