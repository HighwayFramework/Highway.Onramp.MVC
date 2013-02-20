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
using System.Web.Mvc;
using System.Collections.Generic;
using Castle.Core.Logging;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Templates.App_Start.ControllerFactoryWireup), "PostStartup")]
namespace Templates.App_Start
{
    public static class ControllerFactoryWireup
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