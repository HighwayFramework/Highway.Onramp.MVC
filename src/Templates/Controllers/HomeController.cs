using Highway.Data;
using Highway.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Templates.BaseTypes;
using Templates.Entities;

namespace Templates.Controllers
{
    public class HomeController : BaseController
    {
        private IRepositoryFactory factory;

        public HomeController(IRepositoryFactory factory)
        {
            this.factory = factory;
        }

        public ActionResult Index()
        {
            Logger.Debug("Home Controller, checking in sir.");
            var repo = factory.Create<Domain>();
            return View(repo.Find(new FindAll<ExampleEntity>()).ToList());
        }

        public ActionResult BlowUp()
        {
            throw new NotImplementedException("Proper Exception Logged");
        }
    }
}