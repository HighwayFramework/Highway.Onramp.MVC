﻿using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Templates.App_Architecture
{
    public static class IoC
    {
        // Within App_Architecture components, use:
        // #pragma warning disable 618
        // and :
        // #pragma warning restore 618
        // To temporarily supress this warning.
        [Obsolete("Container should never be accessed directly outside of App_Start")]
        public static IWindsorContainer Container { get; set; }
    }
}