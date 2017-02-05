using HR_Management.ShoppingCartBL;
using HR_Management.ShoppingCartBL.ViewModels;
using HR_Management.Web.Common;
using HR_Management.Web.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Controllers
{
    [ChildActionOnly]
    [AllowAnonymous]
    public class LayoutController : Controller
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
        public ActionResult TopLayout()
        {
            return View();
        }
        public ActionResult TopNavBar()
        {
            TopNavbar cartList = new TopNavbar();
            cartList.CartViewModelList = this.RefreshList();
            return View(cartList);
        }
        public ActionResult SearchBar()
        {
            return View();
        }
        public ActionResult MiniCartProduct()
        {
            return View();
        }

        public ActionResult GetMiniCartList(bool isAjaxRequest)
        {
            CartList cartList = new CartList();

            var list = this.RefreshList();
            cartList.CartViewModelList = list;
            cartList.Counter = list.Count();
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
                    html = this.RenderRazorViewToString("_miniCart", cartList)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView("_miniCart", cartList);
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


        public ActionResult LoginModal(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_LoginModal");
        }

        public ActionResult RegisterModal()
        {           
            return PartialView("_SignUpModal");
        }


    }
}