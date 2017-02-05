using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class PromotionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }


        public PromotionResult()
        {
            Success = false;
        }
    }
}