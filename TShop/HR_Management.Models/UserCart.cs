using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
   public class UserCart : AuditableEntity<Guid>
    {       
        public int Qty { get; set; }
        //public decimal Price { get; set; }
        //public decimal? ShippingCharges { get; set; }
        //public decimal? DiscountPer { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
