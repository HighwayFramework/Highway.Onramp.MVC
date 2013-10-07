// [[Highway.Onramp.MVC.Data]]
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
