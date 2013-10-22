using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Templates.App_Architecture.Services;

namespace Templates.App_Architecture.Installers
{
    public class DefaultConventionInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().Pick()
                    .WithServiceDefaultInterfaces()
                    .LifestylePerWebRequest()
                );
        }
    }
}
