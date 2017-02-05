using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class HomeViewModel
    {
        public int? NumberOfCustomers { get; set; }
        public int? NumberOfPremiumnCustomers { get; set; }
        public IEnumerable<CustomerDueFeesViewModel> CustomerDueFeesViewModel { get; set; }
    }
}