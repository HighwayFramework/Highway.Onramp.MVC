// [[Highway.Onramp.MVC.Data]]
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Highway.Data.EventManagement;
using Highway.Data;
using System.Data.Entity;
using Templates.App_Architecture.Data;

namespace Templates.App_Architecture.Installers
{
    // TODO Change the connection string to match your environment.
    public class HighwayDataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRepository>().ImplementedBy<Repository>()
                    .LifestylePerWebRequest(),
                Component.For<IEventManager>().ImplementedBy<EventManager>()
                    .LifestyleSingleton(),
                Component.For<IDatabaseInitializer<HighwayDataContext>>()
                    .ImplementedBy<DropCreateDatabaseAlways<HighwayDataContext>>()
                    .LifestyleSingleton(),
                Component.For<IContextConfiguration>().ImplementedBy<DefaultContextConfiguration>()
                    .LifestyleSingleton()
                );
        }
    }
}
