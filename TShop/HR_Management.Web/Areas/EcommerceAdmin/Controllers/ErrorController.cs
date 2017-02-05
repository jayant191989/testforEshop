using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Areas.EcommerceAdmin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageNotFound(string msg)
        {           
            return View();
        }

        public ActionResult SomethingWentWrong(string msg)
        {
            ViewBag.ErrorMsg = msg;
            return View();
        }
    }
}