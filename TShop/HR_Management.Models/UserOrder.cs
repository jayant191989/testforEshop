using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class UserOrder : AuditableEntity<Guid>
    {
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public decimal TotalAmount { get; set; }
        public string TransactionId { get; set; }
        public string InvoiceNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Guid? StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string StdCode { get; set; }
        public string HomePhone { get; set; }
        public string MobileNo { get; set; }
        public string FirstNameShipping { get; set; }
        public string LastNameShipping { get; set; }
        public string AddressShipping { get; set; }
        public Guid? StateIdShipping { get; set; }
        public string StateShipping { get; set; }
        public string CityShipping { get; set; }
        public string CountryShipping { get; set; }
        public string ZipCodeShipping { get; set; }
        public string StdCodeShipping { get; set; }
        public string PhoneShipping { get; set; }
        public string MobileShipping { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmnt { get; set; }
        public string OrderStatus { get; set; }
        public bool? IsPaid { get; set; }      
       
        public List<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail : AuditableEntity<Guid>
    {
        public Guid UserOrderId { get; set; }
        public virtual UserOrder UserOrder { get; set; }
        public string Username { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

    }
}
