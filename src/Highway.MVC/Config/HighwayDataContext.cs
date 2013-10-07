// [[Highway.Onramp.MVC.Data]]
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
