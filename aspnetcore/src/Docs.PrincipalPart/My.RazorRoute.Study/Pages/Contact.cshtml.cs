﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace My.RazorRoute.Study.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; private set; }

        public string RouteDataTextTemplateValue { get; private set; }

        public void OnGet(string text)
        {
            Message = "Your contact page.";

            if (RouteData.Values["text"] != null)
            {
                RouteDataTextTemplateValue = $"Route data for 'text' was provided: {RouteData.Values["text"]}";
            }
        }
    }
}