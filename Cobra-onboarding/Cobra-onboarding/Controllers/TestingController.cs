using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cobra_onboarding.Controllers
{
    public class TestingController : Controller
    {
        // GET: Testing
        public ActionResult Index(int Id)
        {
            return View(Id);
        }

        public ActionResult TestingPartial(String Id)
        {
            return PartialView();
        }
    }
}