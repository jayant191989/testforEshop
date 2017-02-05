using HR_Management.ShoppingCartBL;
using HR_Management.ShoppingCartBL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Areas.Products.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(string Category, string Search)
        {
            ProductsBL obj = new ProductsBL();
            var lst = obj.GetProductList();
            obj = null;
            ProductLst model = new ProductLst();
            model.ProductViewModelLst = lst;
            return View("ProudctListing", model);
        }

        public ActionResult ProductDetail(Guid Id, bool isAjaxReq)
        {
            ProductsBL obj = new ProductsBL();
            ProductDetailsViewModel model = obj.GetProductDetail(Id);
            //List<ColourModel> colourModel = obj.GetColourByStyleName(model.StyleName);
            //List<SizeModel> sizeModel = obj.GetSizeByStyleName(model.StyleName);
            obj = null;
            if(isAjaxReq==true)
            {
                string modelString = RenderRazorViewToString("_productDetail", model);
                return Json(new { ModelString = modelString }, JsonRequestBehavior.AllowGet);
            }
            
            return View(model);
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
    }
}