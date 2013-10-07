// [[Highway.Onramp.MVC.Logging]]
using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Logging;
using System.Collections.Generic;

namespace Templates.BaseTypes
{
    public class BaseLoggingController : Controller
    {
        public ILogger Logger { get; set; }

        public BaseLoggingController()
        {
            Logger = NullLogger.Instance;
        }
    }
}