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
            modelBuilder.Entity<DeleteMe>();
        }
    }

    // TODO Delete this class once you've created your mappings
    public class DeleteMe
    {
        public int Id { get; set; }
    }
}