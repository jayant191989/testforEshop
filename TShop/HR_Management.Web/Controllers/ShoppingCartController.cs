using HR_Management.Context;
using HR_Management.Models;
using HR_Management.ShoppingCartBL;
using HR_Management.ShoppingCartBL.ViewModels;
using HR_Management.Web.Common;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Controllers
{
    public class ShoppingCartController : Controller
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
        

        public ActionResult ThanksForOrder()
        {
            return View();
        }

        public ActionResult GetCartResultList(bool isAjaxRequest)
        {
            CartList cartList = new CartList();

            cartList.CartViewModelList = this.RefreshList();
            string msg = string.Empty;
            //List<SelectListItem> QtyList = new List<SelectListItem>();
            //for (int count = 1; count <= 10; count++)
            //{
            //    SelectListItem qty = new SelectListItem();
            //    qty.Text = Convert.ToString(count);
            //    qty.Value = Convert.ToString(count);
            //    QtyList.Add(qty);
            //}
            //ViewBag.qtylst = QtyList;
            if (cartList.CartViewModelList.Count() <= 0)
            {
                msg = CommonFunction.ErrorMessage("Cart.", " No product found.");
            }
            if (isAjaxRequest)
            {
                return Json(new
                {
                    msg = msg,
                    html = this.RenderRazorViewToString("_getCartList", cartList)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView("_getCartList", cartList);
            }
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext =
                     new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddToCart(int? color, Guid? productId, int qty)
        {
            var objResponse = new ResponseObject();
            CartList cartList = new CartList();
            if (productId != null)
            {
                ProductsBL objProduct = new ProductsBL();
                var Product = objProduct.GetProductDetail(productId);
                objProduct = null;

                if (Product != null)
                {
                    CartViewModel model = new CartViewModel();
                    model.ProductId = Product.Id;
                    model.SalePrice = Product.SalePrice;
                    string UserId = string.IsNullOrEmpty(Convert.ToString(User.Identity.Name)) ? string.Empty : User.Identity.Name;
                    var user = await UserManager.FindByNameAsync(UserId);
                    model.UserId = user.Id;
                    if (Session["sessionid"] == null)
                    {
                        Session["sessionid"] = Session.SessionID;
                    }
                    model.SessionId = Convert.ToString(Session["sessionid"]);
                    model.Qty = qty;
                    CartBL obj = new CartBL();
                    Guid Id = obj.AddCart(model);
                    obj = null;

                    if (Id != null)
                    {
                        objResponse.IsSuccess = "true";
                        objResponse.StrResponse = CommonFunction.SuccessMessage("Cart.", " Product added successfully.");
                        var list = this.RefreshList();                       
                        cartList.CartViewModelList = list;
                        cartList.Counter = list.Count();
                    }

                    else
                    {
                        objResponse.IsSuccess = "false";
                        objResponse.StrResponse = CommonFunction.ErrorMessage("Cart.", "Oops something went wrong.");
                    }
                }
                else
                {
                    objResponse.IsSuccess = "false";
                    objResponse.StrResponse = CommonFunction.ErrorMessage("Cart.", "Product not found");
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
            return Json(new
            {
                objResponse,
                html = this.RenderRazorViewToString("_miniCart", cartList),
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateCart(Guid? cartId, int Qty)
        {
            var objResponse = new ResponseObject();
            CartList cartList = new CartList();

            if (cartId == null)
            {
                objResponse.IsSuccess = "false";
                objResponse.StrResponse = CommonFunction.ErrorMessage("Cart.", "Something went wrong!");
            }
            else
            {
                string SessionID = string.Empty;
                if (Session["sessionid"] != null)
                {
                    SessionID = Convert.ToString(Session["sessionid"]);
                }
                string userName = string.IsNullOrEmpty(Convert.ToString(User.Identity.Name)) ? string.Empty : User.Identity.Name;
                var user = UserManager.Users.Where(u => u.Email == userName).FirstOrDefault();
                CartBL obj = new CartBL();
                int value = obj.UpdateCart(cartId, SessionID, user, Qty);
                obj = null;
                if (value > 0)
                {
                    objResponse.IsSuccess = "true";
                    objResponse.StrResponse = CommonFunction.SuccessMessage("Cart.", "Product updated successfully.");
                    cartList.CartViewModelList = this.RefreshList();

                }
                else
                {
                    objResponse.IsSuccess = "false";
                    objResponse.StrResponse = CommonFunction.ErrorMessage("Cart.", "Something went wrong!");
                }
            }

            return Json(new
            {
                html = this.RenderRazorViewToString("_getCartList", cartList),
                objResponse
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCart(Guid cartId)
        {
            var objResponse = new ResponseObject();
            CartList cartList = new CartList();

            string SessionID = string.Empty;
            if (Session["sessionid"] == null)
            {
                SessionID = Convert.ToString(Session["sessionid"]);
            }
            string userName = string.IsNullOrEmpty(Convert.ToString(User.Identity.Name)) ? string.Empty : User.Identity.Name;
            var user = UserManager.Users.Where(u => u.Email == userName).FirstOrDefault();
            CartBL obj = new CartBL();
            int value = obj.DeleteCart(cartId, SessionID, user);
            obj = null;
            if (value > 0)
            {
                objResponse.IsSuccess = "true";
                objResponse.StrResponse = CommonFunction.SuccessMessage("Cart.", "Product deleted successfully.");
                cartList.CartViewModelList = this.RefreshList();

            }
            else
            {
                objResponse.IsSuccess = "false";
                objResponse.StrResponse = CommonFunction.ErrorMessage("Cart.", "Something went wrong!");
            }
            return Json(new
            {

                html = this.RenderRazorViewToString("_getCartList", cartList),
                objResponse

            }, JsonRequestBehavior.AllowGet);
        }

        public List<CartViewModel> RefreshList()
        {
            string SessionID = string.Empty;
            if (Session["sessionid"] != null)
            {
                SessionID = Convert.ToString(Session["sessionid"]);
            }
            string userName = string.IsNullOrEmpty(Convert.ToString(User.Identity.Name)) ? string.Empty : User.Identity.Name;
            var user = UserManager.Users.Where(u => u.Email == userName).FirstOrDefault();
            CartBL obj = new CartBL();
            var cartModelLst = obj.GetCartlist(SessionID, user);            
            obj = null;
            return cartModelLst;
        }
    }
}