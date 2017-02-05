using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.ViewModels
{
    public class MembershipViewModel
    {
        public Guid Id { get; set; }
        public string MembershipName { get; set; }
        [Required]
        [Display(Name = "Scheme Start Date")]
        public DateTime? SchemeStartDate { get; set; }
        [Required]
        [Display(Name = "Scheme End Date")]
        public DateTime? SchemeEndDate { get; set; }
        [Required]
        public string TimePeriod { get; set; }
        public decimal Discount { get; set; }
        public decimal? Fees { get; set; }

      
        public List<SelectListItem> getAllDaysList { get; set; }
        public List<SelectListItem> getAllWeekDaysList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="1 Month"},
                 new SelectListItem{ Value="2",Text="3 Month"},
                 new SelectListItem{ Value="3",Text="6 Month"},
                 new SelectListItem{ Value="4",Text="9 Month"},
                 new SelectListItem{ Value="5",Text="1 Year"},
                 new SelectListItem{ Value="6",Text="2 Year"},               
             };
            myList = data.ToList();
            return myList;
        }
    }
}