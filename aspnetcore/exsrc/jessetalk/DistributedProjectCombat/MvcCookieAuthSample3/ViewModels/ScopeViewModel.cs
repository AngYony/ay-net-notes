﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample3.ViewModels
{
    public class ScopeViewModel
    {
        public string Name { get; set; }

        public string DisplayName{ get; set; }

        public string Description{ get; set; }

        public bool Emphasize{ get; set; }


        public bool Required{ get; set; }

        public bool Checked{ get; set; }

    }
}
