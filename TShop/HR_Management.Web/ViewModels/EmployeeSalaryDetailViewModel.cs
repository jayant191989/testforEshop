using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class EmployeeSalaryDetailViewModel
    {
        public Guid Id { get; set; }
        public bool? ESI { get; set; }
        public bool? PF { get; set; }
        public bool? Enrolled { get; set; }
        public string Status { get; set; }
        public decimal? RatePerHour { get; set; }
        public decimal? RatePerHourOvertime { get; set; }
        public double? FixedSalary { get; set; }
        [Display(Name = "OverTime Calculation After Hours")]
        public TimeSpan? OverTimeCal { get; set; }
        public Guid? EmployeeId { get; set; }

    }
}