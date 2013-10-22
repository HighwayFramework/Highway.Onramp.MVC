// [[Highway.Onramp.MVC]]
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Castle.Core.Logging;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof(Templates.App_Architecture.Activators.ControllerFactoryActivator), 
    "PostStartup")]
namespace Templates.App_Architecture.Activators
{
    public static class ControllerFactoryActivator
    {
        public static void PostStartup()
        {
#pragma warning disable 618
            IControllerFactory factory = IoC.Container.Resolve<IControllerFactory>();
#pragma warning restore 618
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}