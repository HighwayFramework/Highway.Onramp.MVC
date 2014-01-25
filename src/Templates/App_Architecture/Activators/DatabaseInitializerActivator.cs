// [[Highway.Onramp.MVC.Data]]
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using Templates.App_Architecture.Activators;
using Templates.App_Architecture.Services.Data;
using Highway.Data;
using Templates.Entities;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof(DatabaseInitializerActivator), 
    "PostStartup")]
namespace Templates.App_Architecture.Activators
{
    public static class DatabaseInitializerActivator
    {
        public static void PostStartup()
        {
#pragma warning disable 618
            var initializer = IoC.Container.Resolve<IDatabaseInitializer<DomainContext<Domain>>>();
#pragma warning restore 618
            Database.SetInitializer(initializer);
        }
    }
}