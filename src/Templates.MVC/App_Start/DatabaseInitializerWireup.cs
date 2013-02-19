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
using System.Collections.Generic;
using Highway.Data;
using System.Data.Entity;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Templates.App_Start.DatabaseInitializerWireup), "PostStartup")]
namespace Templates.App_Start
{
    public static class DatabaseInitializerWireup
    {
        public static void PostStartup()
        {
#pragma warning disable 618
            var initializer = IoC.Container.Resolve<IDatabaseInitializer<DataContext>>();
#pragma warning restore 618
            Database.SetInitializer(initializer);
        }
    }
}