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
        private IConnectionStringConfig connString;
        private IContextConfiguration contextConfig;
        private IMappingConfiguration mappings;

        public Domain(IConnectionStringConfig connString, IContextConfiguration contextConfig, IMappingConfiguration mappings)
        {
            this.connString = connString;
            this.contextConfig = contextConfig;
            this.mappings = mappings;
            Events = new List<IInterceptor>()
            {
                // Any default interceptors can go here.
            };
        }

        public string ConnectionString
        {
            get { return connString.ConnectionString; }
            set { connString.ConnectionString = value; }
        }

        public IContextConfiguration Context
        {
            get { return contextConfig; }
            set { contextConfig = value; }
        }

        public List<IInterceptor> Events { get; set; }

        public IMappingConfiguration Mappings
        {
            get { return mappings; }
            set { mappings = value; }
        }
    }
}
