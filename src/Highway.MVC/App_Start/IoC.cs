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
using System.Linq;
using Castle.Windsor;
using Castle.MicroKernel;
using Castle.Windsor.Installer;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Templates.App_Start.IoC), "Startup")]
namespace Templates.App_Start
{
    public static class IoC
    {
        // Within App_Start components, use:
        // #pragma warning disable 618
        // and :
        // #pragma warning restore 618
        // To temporarily supress this warning.
        [Obsolete("Container should never be accessed directly outside of App_Start")]
        public static IWindsorContainer Container { get; set; }

        public static void Startup()
        {
#pragma warning disable 618
            // Create the container
            Container = new WindsorContainer();

            // Add the Array Resolver, so we can take dependencies on T[]
            // while only registering T.
            Container.Kernel.Resolver.AddSubResolver(new ArrayResolver(Container.Kernel));

            // Register the kernel and container, in case an installer needs it.
            Container.Register(
                Component.For<IKernel>().Instance(Container.Kernel),
                Component.For<IWindsorContainer>().Instance(Container)
                );

            // Search for an use all installers in this application.
            Container.Install(FromAssembly.This());
#pragma warning restore 618
        }
    }
}