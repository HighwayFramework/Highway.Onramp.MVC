// [[Highway.Onramp.MVC]]
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Templates.BaseTypes
{
    public abstract class BaseApiController : ApiController
    {
        public ILogger Logger { get; set; }

        public BaseApiController()
        {
            this.Logger = NullLogger.Instance;
        }
    }

}