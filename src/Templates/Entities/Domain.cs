// [[Highway.Onramp.MVC.Data]]
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Templates.Configs;
using Highway.Data.EventManagement.Interfaces;

namespace Templates.Entities
{
    public class Domain : IDomain
    {
        public Domain(IConnectionStringConfig connString, IMappingConfiguration mappings)
        {
            ConnectionString = connString.ConnectionString;
            Mappings = mappings;
            Context = new DefaultContextConfiguration();
            Events = new List<IInterceptor>()
            {
                // Any default interceptors can go here.
            };
        }

        public string ConnectionString { get; private set; }
        public IContextConfiguration Context { get; private set; }
        public List<IInterceptor> Events { get; private set; }
        public IMappingConfiguration Mappings { get; private set; }
    }
}
