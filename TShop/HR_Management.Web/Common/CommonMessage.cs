using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.Common
{
    public class CommonMessage
    {
        public static string ErrorMessage
        {
            get { return "Unexpected error encountered. Please contact support."; }
        }
        public static string AlreadyExistsMsg(string keyField)
        {
            return keyField + " already exists.";
        }
        public static string InfoNotFound(string key)
        {
            return key + " not found.";
        }
        public static string ReferenceMessage(string keyfield)
        {
            return keyfield + " has related data. Cannot Delete Record.";
        }
        public static string DocumentUploadErrorMessage()
        {
            return "Unexpected Upload Error encountered. Please try again later.";
        }
    }

    public enum CommonMessagetype
    {
        InsertError,
        UpdateError,
        TechnicalError,
        ReferenceError,
    }
}