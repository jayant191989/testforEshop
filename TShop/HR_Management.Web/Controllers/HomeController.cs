using HR_Management.ShoppingCartBL;
using HR_Management.ShoppingCartBL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            HomeViewModel hvm = new HomeViewModel();
            List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
            ProductsBL obj = new ProductsBL();
            var lst = obj.GetProductList();
            obj = null;
            hvm.NewArrivalList = lst;
            return View(hvm);
        }
    }
}