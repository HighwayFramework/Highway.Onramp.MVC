// [[Highway.Onramp.MVC.Data]]
// Copyright 2013 Timothy J. Rayburn
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Highway.Data.Interfaces;
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
