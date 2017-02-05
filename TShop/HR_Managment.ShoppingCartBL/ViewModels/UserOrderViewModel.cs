using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class UserOrderViewModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime OrderDate { get; set; }
        public double ShippingRate { get; set; }
        public string Username { get; set; }

        //[Required(ErrorMessage = "First Name is required")]
        //[DisplayName("First Name")]
        //[StringLength(160)]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is required")]
        //[DisplayName("Last Name")]
        //[StringLength(160)]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Address is required")]
        //[StringLength(70)]
        public string Address { get; set; }

        //[Required(ErrorMessage = "City is required")]
        //[StringLength(40)]
        public string City { get; set; }

        //[Required(ErrorMessage = "State is required")]
        //[StringLength(40)]
        public string State { get; set; }

        //[Required(ErrorMessage = "Postal Code is required")]
        //[DisplayName("Postal Code")]
        //[StringLength(10)]
        public string PostalCode { get; set; }

        //[Required(ErrorMessage = "Country is required")]
        //[StringLength(40)]
        public string Country { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        public string Mobile { get; set; }

        //[Required(ErrorMessage = "Email Address is required")]
        //[DisplayName("Email Address")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        //    ErrorMessage = "Email is is not valid.")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        #region Shipping  start

        public string FirstNameShipping { get; set; }


        public string LastNameShipping { get; set; }


        public string AddressShipping { get; set; }


        public string CityShipping { get; set; }


        public string StateShipping { get; set; }


        public string PostalCodeShipping { get; set; }


        public string CountryShipping { get; set; }



        public string MobileShipping { get; set; }

        public bool? IsPaid { get; set; }

        public string OrderStatus { get; set; }
        public string TransactionId { get; set; }

        #endregion Shipping End

        //[ScaffoldColumn(false)]
        public decimal Total { get; set; }

        //[ScaffoldColumn(false)]
        public string PaymentTransactionId { get; set; }

        //[ScaffoldColumn(false)]
        public bool HasBeenShipped { get; set; }

        public List<OrderDetailViewModel> OrderDetailViewModelList { get; set; }
    }

    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public string Username { get; set; }

        public Guid ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
