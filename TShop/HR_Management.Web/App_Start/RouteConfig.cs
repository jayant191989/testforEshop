using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HR_Management.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string[] namespacesClient = new[] { typeof(HR_Management.Web.Controllers.HomeController).Namespace };
            routes.MapRoute("Home", "", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, null, namespacesClient);
            routes.MapRoute("Login", "Account/Login", new { controller = "Account", action = "Login", id = UrlParameter.Optional }, null, namespacesClient);
            routes.MapRoute("LogOff", "Account/LogOff", new { controller = "Account", action = "LogOff", id = UrlParameter.Optional }, null, namespacesClient);
            routes.MapRoute("Register", "Account/Register", new { controller = "Account", action = "Register", id = UrlParameter.Optional }, null, namespacesClient);

            routes.MapRoute("AdminLogin", "TOIManagement/Account/Login", new
            {
                controller = "Account",
                action = "Login",
                id = UrlParameter.Optional
            }, namespaces: new[] { "HR_Management.Web.Areas.TOIManagement.Controllers" });

            routes.MapRoute("HomeTOIManagement", "TOIManagement/Home/Index", new
            {
                controller = "Account",
                action = "Login",
                id = UrlParameter.Optional
            }, namespaces: new[] { "HR_Management.Web.Areas.TOIManagement.Controllers" });

            routes.MapRoute("HomeEcommerceAdmin", "EcommerceAdmin/Home/Index", new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }, namespaces: new[] { "HR_Management.Web.Areas.EcommerceAdmin.Controllers" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HR_Management.Web.Controllers" }
            );
        }
    }
}
