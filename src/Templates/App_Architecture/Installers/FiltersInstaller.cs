// [[Highway.Onramp.MVC]]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Mvc;
using Templates.App_Architecture.Services;
using Templates.App_Architecture.Filters;

namespace Templates.App_Architecture.Installers
{
    public class FiltersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<ControllerContext,ActionDescriptor,Filter>>().Instance(
                    (c,a) => new Filter(container.Resolve<ExceptionLoggingFilter>(), FilterScope.Last, int.MinValue))
                );
        }
    }
}
