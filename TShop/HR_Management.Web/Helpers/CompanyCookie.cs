using HR_Management.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.Helpers
{
    public class CompanyCookie
    {
       
        public static void companyCookie(Guid companyId)
        {            
            HttpCookie cmpCookie = new HttpCookie("cmpCookie"); // creating cookie
            cmpCookie.Values["CompanyID"] = companyId.ToString();
            cmpCookie.Expires = DateTime.Now.AddDays(7); // persisting cookie for 7 days
            System.Web.HttpContext.Current.Response.Cookies.Add(cmpCookie); // adding cookie to end user's pc
        }


        public static Guid CompId
        {
            get
            {
                if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Request.Cookies["cmpCookie"]["CompanyID"])) == false)
                {
                    return Guid.Parse(HttpContext.Current.Request.Cookies["cmpCookie"]["CompanyID"]);
                }
                else
                {
                    return Guid.Empty;
                }

            }
        }
        //ablCookie.Values["ID"] = objTbl.iID.ToString();
        //ablCookie.Values["TYPE"] = objTbl.vType;
        //ablCookie.Values["NAME"] = objTbl.vName;
        //ablCookie.Values["BranchId"] =Convert.ToString( objTbl.iBranchID);
        //ablCookie.Values["YEARID"] = Convert.ToString(YearId);
        //ablCookie.Values["CompId"] = Convert.ToString(objTbl.iCompID);
        //ablCookie.Expires = DateTime.Now.AddDays(7); // persisting cookie for 7 days
        //System.Web.HttpContext.Current.Response.Cookies.Add(ablCookie); // adding cookie to end user's pc
    }
}