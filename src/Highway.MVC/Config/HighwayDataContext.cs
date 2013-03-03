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

using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Common.Logging;

namespace Templates.Config
{
    public class HighwayDataContext : DataContext
    {
        public HighwayDataContext(string connectionString, IMappingConfiguration mapping)
            : base(connectionString, mapping)
        {
        }

        public HighwayDataContext(string connectionString, IMappingConfiguration mapping, ILog log)
            : base(connectionString, mapping, log)
        {
        }

        public HighwayDataContext(string connectionString, IMappingConfiguration mapping, IContextConfiguration contextConfiguration)
            : base(connectionString, mapping, contextConfiguration)
        {
        }

        public HighwayDataContext(string connectionString, IMappingConfiguration mapping, IContextConfiguration contextConfiguration, ILog log)
            : base(connectionString, mapping, contextConfiguration, log)
        {
        }

        public HighwayDataContext(string databaseFirstConnectionString)
            : base(databaseFirstConnectionString)
        {
        }

        public HighwayDataContext(string databaseFirstConnectionString, ILog log)
            : base(databaseFirstConnectionString, log)
        {
        }
    }
}
