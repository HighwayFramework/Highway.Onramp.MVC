// [[Highway.Onramp.MVC]]
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
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Mvc;
using Templates.Filters;
using Templates.Services;

namespace Templates.Installers
{
    public class FilterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IFilterProvider>().ImplementedBy<IoCFilterProvider>(),
                Component.For<ExceptionLoggingFilter>().ImplementedBy<ExceptionLoggingFilter>(),
                Component.For<Func<ControllerContext,ActionDescriptor,Filter>>().Instance(
                    (c,a) => new Filter(container.Resolve<ExceptionLoggingFilter>(), FilterScope.Last, int.MinValue))
                );
        }
    }
}
