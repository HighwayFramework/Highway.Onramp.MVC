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
using Templates.Config;

namespace Templates.Installers
{
    // TODO Change the connection string to match your environment.
    public class HighwayDataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataContext>().ImplementedBy<HighwayDataContext>()
                    .DependsOn(new
                    {
                        connectionString = @"Data Source=.;Initial Catalog=ChangeMyConnectionString;Integrated Security=SSPI;"
                    })
                    .LifestylePerWebRequest(),
                Component.For<IRepository>().ImplementedBy<Repository>()
                    .LifestylePerWebRequest(),
                Component.For<IEventManager>().ImplementedBy<EventManager>()
                    .LifestyleSingleton(),
                Component.For<IMappingConfiguration>().ImplementedBy<HighwayMappings>()
                    .LifestyleSingleton(),
                Component.For<IDatabaseInitializer<HighwayDataContext>>().ImplementedBy<HighwayDatabaseInitializer>()
                    .LifestyleSingleton(),
                Component.For<IContextConfiguration>().ImplementedBy<HighwayContextConfiguration>()
                    .LifestyleSingleton()
                );
        }
    }
}
