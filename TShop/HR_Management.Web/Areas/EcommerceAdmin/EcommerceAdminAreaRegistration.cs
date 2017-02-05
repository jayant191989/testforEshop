using System.Web.Mvc;

namespace HR_Management.Web.Areas.EcommerceAdmin
{
    public class EcommerceAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EcommerceAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EcommerceAdmin_default",
                "EcommerceAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}