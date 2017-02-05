using HR_Management.Models;
using HR_Management.ShoppingCartBL;
using HR_Management.ShoppingCartBL.ViewModels;
using HR_Management.Web.Common;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Controllers
{
    public class CheckOutController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckOutOnePage()
        {
            CheckOutBL checkOutBL = new CheckOutBL();
            var addresses = checkOutBL.GetAddressList(User.Identity.Name);
            ViewBag.AddressTitle = new SelectList(addresses, "Id", "AddressTitle");
            return View();
        }

        [HttpPost]
        public ActionResult CheckOutOnePage(CheckOutViewModel model)
        {
            var objResponse = new ResponseObject();
            string msg = string.Empty;
            if (model != null && ModelState.IsValid)
            {
                string userName = string.IsNullOrEmpty(Convert.ToString(User.Identity.Name)) ? string.Empty : User.Identity.Name;

                if (string.IsNullOrEmpty(userName))
                {
                    ViewBag.msg = "Please login for your order confirmation";
                    return View("Index");
                }
                else
                {
                    Session["invoiceno"] = Guid.NewGuid();
                    string SessionID = string.Empty;
                    if (Session["sessionid"] != null)
                    {
                        SessionID = Convert.ToString(Session["sessionid"]);
                    }

                    CartBL objcart = new CartBL();
                    Int64 value = objcart.UpdateCartWithUserId(SessionID, userName);
                    objcart = null;
                    Session["checkoutmodel"] = model;
                    //UserRepo objuser = new UserRepo();
                    //int value1 = objuser.UpdateUserInfo(model.BillingModel);
                    //objuser = null;
                    return RedirectToAction("PayNowCart", model);
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                objResponse.IsSuccess = "false";
                objResponse.StrResponse = CommonFunction.ErrorMessage(CommonMessagetype.TechnicalError.ToString(), CommonMessage.ErrorMessage);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult PayNowCart(CheckOutViewModel checkoutViewModel)
        {
            if (checkoutViewModel != null && ModelState.IsValid)
            {
                string SessionID = string.Empty;
                if (Session["sessionid"] != null)
                {
                    SessionID = Convert.ToString(Session["sessionid"]);
                }
                string userName = string.IsNullOrEmpty(Convert.ToString(User.Identity.Name)) ? string.Empty : User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    ViewBag.msg = "Please login for your order confirmation";
                    return View("Index");
                }
                else
                {
                    Session["invoiceno"] = Guid.NewGuid();
                    Session["checkoutmodel"] = checkoutViewModel;
                    CartBL objcart = new CartBL();
                    Int64 value = objcart.UpdateCartWithUserId(SessionID, userName);
                    objcart = null;

                    //UserRepo objuser = new UserRepo();
                    //int value1 = objuser.UpdateUserInfo(model.BillingModel);
                    //objuser = null;
                    // return RedirectToAction("PayNowCart", model);
                }
                UserRepo objuser = new UserRepo();
                var user = objuser.GetUserInfo(userName);
                //var user = UserManager.Users.Where(u => u.Email == userName).FirstOrDefault();

                CartBL obj = new CartBL();
                var cartModelLst = obj.GetCartlist(SessionID, user);
                obj = null;

                string Email = user.Email;
                UserOrderViewModel orderViewModel = new UserOrderViewModel();
                orderViewModel.Address = checkoutViewModel.BillingModel.AddressBlock;
                orderViewModel.City = checkoutViewModel.BillingModel.City;
                orderViewModel.Country = checkoutViewModel.BillingModel.Country;
                orderViewModel.FirstName = checkoutViewModel.BillingModel.FirstName;
                orderViewModel.LastName = checkoutViewModel.BillingModel.LastName;
                orderViewModel.PostalCode = checkoutViewModel.BillingModel.ZipCode;
                orderViewModel.State = checkoutViewModel.BillingModel.State;
                orderViewModel.Mobile = checkoutViewModel.BillingModel.Mobile;

                orderViewModel.AddressShipping = checkoutViewModel.ShippingModel.AddressBlock;
                orderViewModel.CityShipping = checkoutViewModel.ShippingModel.City;
                orderViewModel.CountryShipping = checkoutViewModel.ShippingModel.Country;
                orderViewModel.FirstNameShipping = checkoutViewModel.ShippingModel.FirstName;
                orderViewModel.LastNameShipping = checkoutViewModel.ShippingModel.LastName;
                orderViewModel.PostalCodeShipping = checkoutViewModel.ShippingModel.ZipCode;
                orderViewModel.StateShipping = checkoutViewModel.ShippingModel.State;
                orderViewModel.MobileShipping = checkoutViewModel.ShippingModel.Mobile;

                orderViewModel.OrderDate = DateTime.Now;
                orderViewModel.InvoiceNo = Convert.ToString(Session["invoiceno"]);
                orderViewModel.Email = Email;
                orderViewModel.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                orderViewModel.UserId = user.Id;

                List<OrderDetailViewModel> orderDetailViewModelList = new List<OrderDetailViewModel>();
                string detl = "<table border='1'>";
                detl += "<thead>";
                detl += "<tr>";
                detl += "<th>Srno</th>";
                detl += "<th>Product</th>";
                detl += "<th>Qty</th>";
                detl += "<th>Price</th>";
                detl += "<th>Amount</th>";
                detl += "</tr>";
                detl += "</thead>";
                detl += "<tbody>";
                int srno = 1;
                decimal totalAmount = 0;
                foreach (var cart in cartModelLst)
                {
                    OrderDetailViewModel od = new OrderDetailViewModel();
                    od.ProductId = cart.ProductId;
                    od.Quantity = cart.Qty;
                    od.UnitPrice = cart.SalePrice;
                    od.Discount = 0;
                    orderDetailViewModelList.Add(od);
                    detl += "<tr>";
                    detl += "<td>" + srno++ + "</td>";
                    detl += "<td>" + cart.ProductName + "</td>";
                    detl += "<td>" + cart.Qty + "</td>";
                    detl += "<td> Rs." + String.Format("{0:0.00}", cart.SalePrice) + "</td>";
                    detl += "<td> Rs." + String.Format("{0:0.00}", cart.Qty * cart.SalePrice) + "</td>";
                    detl += "</tr>";
                    totalAmount += (cart.Qty * cart.SalePrice);
                }
                detl += "<tr>";
                detl += "<td colspan='4' align='right'>Sub Total</td>";
                detl += "<td>" + String.Format("{0:0.00}", totalAmount) + "</td>";
                detl += "</tr>";
                detl += "</tbody>";
                detl += "</table>";
                orderViewModel.OrderDetailViewModelList = orderDetailViewModelList;
                orderViewModel.Total = Convert.ToDecimal(totalAmount);

                // remove detail from cart
                OrderBL objOrder = new OrderBL();
                Int64 OrderId = objOrder.AddToOrder(orderViewModel);
                objOrder = null;
                //CartRepoModel objCart = new CartRepoModel();
                //int value = objCart.RemoveFromCart(UserId);
                //objCart = null;


                // mail to admin
                #region "Send mail by admin"
                string htmlString = CommonFunction.ReadHtmlTemplate("order");

                htmlString = htmlString.Replace("@@sitename@@", CommonFunction.SiteName)
                                       .Replace("@@emailid@@", Email).Replace("@@orderdate@@", orderViewModel.OrderDate.ToString("dd-MM-yyyy"))
                                       .Replace("@@invoiceno@@", orderViewModel.InvoiceNo)
                                       .Replace("@@username@@", orderViewModel.FirstName + " " + orderViewModel.LastName)
                                       .Replace("@@detail@@", detl);
                // var flag = CF.SendMail(CommonFunction.AdminEmail, CommonFunction.AdminEmail, "Order place", htmlString);
                // CommonFunction.SendEmail(Email, "Confirm your account",htmlString);
                CommonFunction.SendByAdminGmail(Email, "Order place", htmlString);

                #endregion

                // redirect to payment gateway

                return RedirectToAction("Index", "Thanks");
            }
            else
            {

            }
            return View();
        }
    }
}