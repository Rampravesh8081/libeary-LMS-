﻿using System.Web;
using System.Web.Mvc;

namespace LMS_Library_Management_System_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
