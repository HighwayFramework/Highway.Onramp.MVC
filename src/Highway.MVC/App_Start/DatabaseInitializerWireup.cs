// [[Highway.Onramp.MVC.Data]]
using System;
using System.Linq;
using System.Collections.Generic;
using Highway.Data;
using System.Data.Entity;
using Templates.Config;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Templates.App_Start.DatabaseInitializerWireup), "PostStartup")]
namespace Templates.App_Start
{
    public static class DatabaseInitializerWireup
    {
        public static void PostStartup()
        {
#pragma warning disable 618
            var initializer = IoC.Container.Resolve<IDatabaseInitializer<HighwayDataContext>>();
#pragma warning restore 618
            Database.SetInitializer(initializer);
        }
    }
}