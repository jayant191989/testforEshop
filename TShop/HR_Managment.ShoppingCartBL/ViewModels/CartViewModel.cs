using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public Int64 Number { get; set; }
        public Guid ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string ProductDescription { get; set; }
        public double Mrp { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DisPer { get; set; }
        public int Qty { get; set; }
        public decimal? Total { get; set; }
        public decimal? ShippingCharges { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string ProductImage { get; set; }
        public string ImagePath { get; set; }
    }
    public class CartList
    {
        public int Counter { get; set; }
        public IEnumerable<CartViewModel> CartViewModelList { get; set; }

    }
}
