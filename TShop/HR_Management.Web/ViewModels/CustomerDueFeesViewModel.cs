using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class CustomerDueFeesViewModel
    {       
        public string MembershipName { get; set; }
        public decimal? MembershipFees { get; set; }
        public DateTime? MembershipJoinDate { get; set; }
        public string CustomerName { get; set; }
        public decimal? DueFees { get; set; }
        public int DueTime { get; set; }
        public string Status { get; set; }
    }
}