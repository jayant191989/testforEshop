using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class EmployeeSalaryByViewModel
    {
        public Guid Id { get; set; }
        public int? Month { get; set; }
        public string FullName { get; set; }
        public decimal? Total { get; set; }
        public IEnumerable<SalaryByEmployeeViewModel> SalaryByEmployeeViewModel { get; set; }   
    }
}