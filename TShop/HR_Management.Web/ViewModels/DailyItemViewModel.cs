using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class DailyItemViewModel
    {
        public Guid Id { get; set; }
        public string BatchName { get; set; }
        public string Code { get; set; }
        public string ModelNumber { get; set; }
        public string AutoGenerateName { get; set; }
        public string Name { get; set; }
        public decimal? DiscountRatePerUnit { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public Guid StoreProductId { get; set; }      
        public Guid DailyId { get; set; }
        public Guid BatchId { get; set; }

    }
}