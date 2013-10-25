// [[Highway.Onramp.MVC.Data]]
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Highway.Data;

namespace Templates.App_Architecture.PlugIns.Data
{
    public class HighwayMappingConfiguration : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            // TODO Create Mappings Here!
            modelBuilder.Entity<ExampleEntity>();
        }
    }
}