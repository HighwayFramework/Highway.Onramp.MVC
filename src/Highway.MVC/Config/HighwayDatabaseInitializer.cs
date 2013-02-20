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
using System.Data.Entity;
using System.Linq;
using Castle.Core.Logging;
using Highway.Data;

namespace Templates.Config
{
    // Remove the obsolete attribute once you've addressed this change.
    // TODO Change the base class for this to an Initializer that matches your strategy.
    public class HighwayDatabaseInitializer : DropCreateDatabaseAlways<HighwayDataContext>
    {
        public ILogger Logger { get; set; }

        public HighwayDatabaseInitializer() 
        {
            Logger = NullLogger.Instance;
        }

        protected override void Seed(HighwayDataContext context)
        {
            Logger.Info("Seeding Database");
            base.Seed(context);
        }
    }
}
