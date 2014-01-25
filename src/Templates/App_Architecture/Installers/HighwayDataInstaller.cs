// [[Highway.Onramp.MVC.Data]]
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Highway.Data;
using Highway.Data.Factories;
using Highway.Data.Repositories;
using Templates.App_Architecture.Services.Data;

namespace Templates.App_Architecture.Installers
{
    public class HighwayDataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRepositoryFactory>().ImplementedBy<RepositoryFactory>());
        }
    }
}
