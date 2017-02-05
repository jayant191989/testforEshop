using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class CheckOutViewModel
    {
        public UserAddressViewModel ShippingModel { get; set; }
        public UserAddressViewModel BillingModel { get; set; }
        public string BillingAddressValue { get; set; }
        public string ShippingAddressValue { get; set; }
    }
}
