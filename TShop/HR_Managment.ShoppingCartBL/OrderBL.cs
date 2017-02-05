using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR_Management.Context;
using HR_Management.Models;
using System.Transactions;
using HR_Management.ShoppingCartBL.ViewModels;

namespace HR_Management.ShoppingCartBL
{
    public class OrderBL
    {
        public Int64 AddToOrder(UserOrderViewModel userOrderViewModel)
        {
            Int64 OrderId = 0;

            using (var txn = new TransactionScope())
            {
                try
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        UserOrder order = new UserOrder();
                        Guid orderID = Guid.NewGuid();
                        order.Id = orderID;
                        order.UserId = userOrderViewModel.UserId;
                        order.InvoiceNo = userOrderViewModel.InvoiceNo;
                        order.Email = userOrderViewModel.Email;
                        order.Ip = userOrderViewModel.IpAddress;
                        order.OrderStatus = "under process";
                        order.IsPaid = false;

                        order.Address = userOrderViewModel.Address;
                        order.City = userOrderViewModel.City;
                        order.Country = userOrderViewModel.Country;
                        order.FirstName = userOrderViewModel.FirstName;
                        order.LastName = userOrderViewModel.LastName;
                        order.MobileNo = userOrderViewModel.Mobile;
                        order.StateName = userOrderViewModel.State;
                        order.ZipCode = userOrderViewModel.PostalCode;

                        order.FirstNameShipping = userOrderViewModel.FirstNameShipping;
                        order.LastNameShipping = userOrderViewModel.LastNameShipping;
                        order.AddressShipping = userOrderViewModel.AddressShipping;
                        order.CityShipping = userOrderViewModel.CityShipping;
                        order.StateShipping = userOrderViewModel.StateShipping;
                        order.CountryShipping = userOrderViewModel.CountryShipping;
                        order.ZipCodeShipping = userOrderViewModel.PostalCodeShipping;
                        order.MobileShipping = userOrderViewModel.MobileShipping;
                        // order.StateIdShipping = userOrderViewModel.;
                        order.StdCodeShipping = string.Empty;
                        order.PhoneShipping = string.Empty;

                        order.CouponCode = string.Empty;
                        order.TotalAmount = userOrderViewModel.Total;

                        order.DiscountAmnt = 0;
                        //  order.StateId = 0;
                        order.StdCode = string.Empty;
                        order.TransactionId = string.Empty;
                        order.UserId = userOrderViewModel.UserId;
                        order.HomePhone = string.Empty;

                        db.UserOrders.Add(order);
                        db.SaveChanges();
                        OrderId = 1;

                        foreach (var od in userOrderViewModel.OrderDetailViewModelList)
                        {
                            OrderDetail orderDetail = new OrderDetail();
                            orderDetail.Discount = od.Discount;
                            orderDetail.UserOrderId = orderID;
                            orderDetail.ProductId = od.ProductId;
                            orderDetail.Quantity = od.Quantity;
                            orderDetail.UnitPrice = od.UnitPrice;
                            db.OrderDetails.Add(orderDetail);
                            db.SaveChanges();

                        }
                        /// remove detail from tblcart
                        var cart = db.UserCarts.Where(w => w.UserId == order.UserId).ToList();
                        if (cart != null)
                        {
                            foreach (var c in cart)
                            {
                                db.UserCarts.Remove(c);
                                db.SaveChanges();
                            }
                        }

                        txn.Complete();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return OrderId;
        }

      
    }
}
