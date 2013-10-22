// [[Highway.Onramp.MVC.Logging]]
using System;
using System.Linq;
using System.Collections.Generic;
using Castle.Core.Logging;
using System.Data.Entity;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Templates.App_Architecture.LoggerAnnouncementsWireup), "PostStartup")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(Templates.App_Architecture.LoggerAnnouncementsWireup), "Shutdown")]
namespace Templates.App_Architecture
{
    public static class LoggerAnnouncementsWireup
    {
        private static ILogger logger = NullLogger.Instance;
        public static void PostStartup()
        {
#pragma warning disable 618
            logger = IoC.Container.Resolve<ILogger>();
#pragma warning restore 618

            logger.Info("Application Startup Completed");
        }

        public static void Shutdown()
        {
            logger.Info("Application Shutdown");
        }
    }
}