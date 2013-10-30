using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Templates.App_Architecture.BaseTypes;
using Templates.App_Architecture.PlugIns.Data;
using Templates.Entities;

namespace Templates.Controllers
{
    public class HomeController : BaseController
    {
        private IRepository repo;

        public HomeController(IRepository repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            Logger.Debug("Home Controller, checking in sir.");
            return View(repo.Context.AsQueryable<ExampleEntity>().ToList());
        }

        public ActionResult BlowUp()
        {
            throw new NotImplementedException("Proper Exception Logged");
        }
    }
}