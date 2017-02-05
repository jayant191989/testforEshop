using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class ContactPaymentHistory
    {
        public decimal? Total { get; set; }
        public decimal? TotalAmountTaken { get; set; }
        public decimal? OutStanding { get; set; }
        public IEnumerable<DailyViewModel> DailyItemViewModel { get; set; }
    }
}