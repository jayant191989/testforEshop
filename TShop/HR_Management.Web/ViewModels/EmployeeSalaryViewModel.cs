using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class EmployeeSalaryViewModel
    {
        public DateTime Date { get; set; }
        public Guid SalaryId { get; set; }
        public string FullName { get; set; }       
        public decimal? WorkHours { get; set; }
        public decimal? OverTimeHours { get; set; }
        public decimal? RatePerHour { get; set; }
        public decimal? RatePerHourOvertime { get; set; }
        public decimal? WorkHourTotal { get; set; }
        public decimal? OverTimeTotal { get; set; }
        public string SalaryList { get; set; }
      
    }
}