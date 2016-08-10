using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductTest.Models;

namespace ProductTest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        DataEntities db = new DataEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Table product)
        {
            Table test = db.Tables.Find(product.Id);
            return View(test);
        }

    }
}
