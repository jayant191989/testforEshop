using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.ViewModels
{
    public class DailyViewModel
    {
        public Guid Id { get; set; }
        public string Head { get; set; }
        public decimal? Invoice { get; set; }
        public DateTime? Date { get; set; }
        public Guid ContactId { get; set; }
        public Guid? MembershipId { get; set; }
        public string Ledger { get; set; }
        public string CustomerName { get; set; }
        public Guid ParticularId { get; set; }
        public string Particular { get; set; }
        public string Note { get; set; }
        public decimal? ProductsTotal { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Net { get; set; }
        public decimal? Due { get; set; }
        public decimal? HdnDue { get; set; }
        public decimal? OutStandingAmount { get; set; }
        public decimal? Total { get; set; }
        public List<SelectListItem> GetHeadList { get; set; }
        public List<SelectListItem> getList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                new SelectListItem{ Value="1",Text="Service"},
                 new SelectListItem{ Value="2",Text="Sales"},
                 new SelectListItem{ Value="3",Text="Expense"},                
                 new SelectListItem{ Value="4",Text="Revenue"}
             };
            myList = data.ToList();
            return myList;
        }
        public int X { get; set; }
        public virtual IList<DailyItemViewModel> DailyItemViewModels { get; set; }
    }

    public class DailyViewModelResult
    {
        public IEnumerable<DailyViewModel> DailyViewModelList { get; set; }
    }

}