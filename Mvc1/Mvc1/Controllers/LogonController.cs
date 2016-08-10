using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc1.Controllers;
using Mvc1.Models;

namespace Mvc1.Controllers
{
    public class LogonController : Controller
    {
        //
        // GET: /Logon/

        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Status(Logon logon)
        {         
            return View(logon);
        }
    }
}
