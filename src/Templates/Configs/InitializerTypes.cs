// [[Highway.Onramp.MVC.Data]]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Templates.Configs
{
    public enum InitializerTypes
    {
        DropCreateDatabaseAlways,
        DropCreateDatabaseIfModelChanges,
        CreateDatabaseIfNotExists,
        NullDatabaseInitializer
    }
}
